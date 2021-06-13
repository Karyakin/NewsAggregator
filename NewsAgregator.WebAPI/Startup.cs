using Contracts.RepositoryInterfaces;
using Contracts.ServicesInterfacaces;
using Contracts.UnitOfWorkInterface;
using Entities.DataTransferObject;
using Entities.Models;
using Hangfire;
using Hangfire.SqlServer;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using NewsAgregato.DAL.CQRS.QueryHendlers;
using Repositories.CommentRepo;
using Repositories.Context;
using Repositories.CountryRepo;
using Repositories.NewsRep;
using Repositories.UnitOfWorkRepository;
using Services;
using Services.Parsers;
using Services.SQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NewsAgregator.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<NewsDataContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("SqlConnectionStr"),
                    x => x.MigrationsAssembly(typeof(NewsDataContext).Assembly.FullName));
            });

            services.AddControllersWithViews();
            services.AddScoped<IUnitOfWork, RepositoryUnitOfWork>();
            services.AddScoped<INewsService, NewsService>();
          /*  services.AddScoped<INewsService, CQRSNewsService>();*/
            services.AddScoped<IRssSourceService, RssSourceService>();
            services.AddScoped<IRssSourceService, CQSRssSourceService>();
            /*  services.AddScoped<IUserService, CQRSUserService>();*/


            services.AddScoped<ICategoryService, CategoryService>();


            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IPhoneService, PhoneService>();
            services.AddScoped<IContactDetailsService, ContactDetailsService>();
            services.AddScoped<ICommentService, CommentService>();


            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();



            services.AddScoped<TutByParser>(); //внедрение без привязки к родитель(альтернатива)
            services.AddScoped<OnlinerParser>();//внедрение без привязки к родитель(альтернатива)

            services.AddMediatR(typeof(GetRssSourseByIdQueryHendler).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(GetRssSourseByNameAndUrlHendler).GetTypeInfo().Assembly);


            services.AddHangfire(conf => conf// для автоматического обновления новосте
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(Configuration.GetConnectionString("SqlConnectionStr"), new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(30),// время на выполнение команды
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(30),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                }));

            services.AddHangfireServer();// добавляем сервер хэндфаера


            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(GetRssSourseByIdQueryHendler).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(GetRssSourseByNameAndUrlHendler).GetTypeInfo().Assembly);


            services.AddControllers().AddJsonOptions(x =>x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

            services.AddAutoMapper(typeof(MappingProfile).Assembly);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "NewsAgregator.WebAPI", Version = "v1" });
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NewsAgregator.WebAPI v1"));
            }

            app.UseHangfireDashboard();
            var newsService = serviceProvider.GetService(typeof(INewsService)) as INewsService;
            /* var rssService = serviceProvider.GetService(typeof(IRssSourceService)) as IRssSourceService;*/
            /*
                        RecurringJob.AddOrUpdate(() =>  rssService.GetAllRssSourceAsync(false), "0,15,30,45 * * * *"); */

           /* RecurringJob.AddOrUpdate(() => newsService.Aggregate(), "0,15,30,30 * * * *");*/
            RecurringJob.AddOrUpdate(() => newsService.RateNews(), "0,15,30,30 * * * *");
            RecurringJob.AddOrUpdate(() => Console.WriteLine("выполнилась джоба"), "0,17,20,30,45 * * * *");//crontab.guru

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHangfireDashboard();//для hangFire
            });
        }
    }
}
