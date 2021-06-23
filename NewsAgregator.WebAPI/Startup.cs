using Contracts.RepositoryInterfaces;
using Contracts.ServicesInterfacaces;
using Contracts.UnitOfWorkInterface;
using Entities.DataTransferObject;
using Hangfire;
using Hangfire.SqlServer;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NewsAgregato.DAL.CQRS.QueryHendlers;
using NewsAgregator.WebAPI.Auth;
using Repositories.CommentRepo;
using Repositories.Context;
using Repositories.CountryRepo;
using Repositories.NewsRep;
using Repositories.UnitOfWorkRepository;
using Services;
using Services.Parsers;
using Services.SQRS;
using System;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

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
           // services.AddScoped<INewsService, CQRSNewsService>();
            services.AddScoped<IRssSourceService, RssSourceService>();
            services.AddScoped<IRssSourceService, CQSRssSourceService>();
            services.AddScoped<IUserService, CQRSUserService>();
            services.AddScoped<IRoleService, CQRSRoleService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IPhoneService, PhoneService>();
            services.AddScoped<IContactDetailsService, ContactDetailsService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IJwtAuthManager, JwtAuthManager>();
            services.AddScoped<TutByParser>(); //внедрение без привязки к родитель(альтернатива)
            services.AddScoped<OnlinerParser>();//внедрение без привязки к родитель(альтернатива)
            services.AddScoped<IgromaniaParser>();//внедрение без привязки к родитель(альтернатива)

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
            services.AddMediatR(typeof(GetUserByLoginQueryHendler).GetTypeInfo().Assembly);

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(opt =>
                {
                    opt.RequireHttpsMetadata = false;
                    opt.SaveToken = true;//сохранение токена
                    opt.TokenValidationParameters = new TokenValidationParameters()// парметра валидации токена
                    {
                        ValidateIssuerSigningKey = true,//токен будет шифрованным
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),//шифровка для токена
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

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

            /*https://crontab.guru/#*_*_*_*_**/
            RecurringJob.AddOrUpdate(() => newsService.RateNews(), "59 * * * *");
            /* RecurringJob.AddOrUpdate(() => newsService.Aggregate(), "* 6,10,14,20,23 * * *");
            */
            /*RecurringJob.AddOrUpdate(() => Console.WriteLine("выполнилась джоба"), "0,17,20,30,45 * * * *");//crontab.guru*/

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHangfireDashboard();//для hangFire
            });
        }
    }
}
