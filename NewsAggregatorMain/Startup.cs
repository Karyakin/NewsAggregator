using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repositories.UnitOfWorkRepository;
using Contracts.UnitOfWorkInterface;
using Entities.DataTransferObject;
using Repositories.Context;
using Contracts.ServicesInterfacaces;
using Services;
using Services.Parsers;
using NewsAggregatorMain.Filters;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Contracts.RepositoryInterfaces;
using Repositories.NewsRep;
using Repositories.CountryRepo;
using Microsoft.AspNetCore.Authorization;
using NewsAggregatorMain.AuthorizationPolicies;
using Repositories.CommentRepo;
using NewsAgregato.DAL.CQRS.QueryHendlers;
using System.Reflection;
using MediatR;
using System;
using Services.�urrencyExchangeHelpers;
using NewsAggregatorMain.Helper;
using Services.ServiseHelpers;


/*https://www.youtube.com/watch?v=M5kbqNaT4eA&ab_channel=ITAcademyWebinar*/
/*cd C:\Users\d.karyakin\Desktop\NewsAggregator\RepositoryBase*/

/*PS C:\Users\d.karyakin\Desktop\NewsAggregator\RepositoryBase> dotnet ef --startup-project ../NewsAggregatorMain/ migration
s add Initial*/

/*PS C:\Users\d.karyakin\Desktop\NewsAggregator\RepositoryBase > dotnet ef--startup - project.. / NewsAggregatorMain / databas
e update*/

namespace NewsAggregatorMain
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
            services.AddRazorPages().AddRazorRuntimeCompilation();

            services.AddDbContext<NewsDataContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("SqlConnectionStr"),
                    x => x.MigrationsAssembly(typeof(NewsDataContext).Assembly.FullName));
            });

            services.AddHttpClient<IExchangeService, ExchangeService>(client =>
            {
                client.BaseAddress = new Uri(Configuration.GetSection("NationalBankSettings")["BaseUrl"]);
            });

            services.Configure<NationalBankSettings>(Configuration.GetSection("NationalBankSettings"));

            services.AddControllersWithViews();
            services.AddScoped<IUnitOfWork, RepositoryUnitOfWork>();
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<IRssSourceService, RssSourceService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IUserService, UserService>();
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
            services.AddScoped<TutByParser>(); //��������� ��� �������� � ��������(������������)
            services.AddScoped<OnlinerParser>();//��������� ��� �������� � ��������(������������)
            services.AddScoped<IgromaniaParser>();//��������� ��� �������� � ��������(������������)
            services.AddScoped<OONParser>();//��������� ��� �������� � ��������(������������)
            services.AddScoped<ITexterra,Texterra>();

            services.AddMediatR(typeof(GetRssSourseByIdQueryHendler).GetTypeInfo().Assembly);

            services.AddControllersWithViews()
                .AddMvcOptions(opt =>
                {
                    opt.Filters.Add(new ChromFilterAttribute());
                    opt.Filters.Add(new CustomExceptionFilterAttribite());
                });// ���������� ����� ��������


            services.AddScoped<CheckDataFilterAttribute>();// ��������� ������������ ��� �������
                                                           // services.AddScoped<CustomExceptionFilterAttribite>();// ��������� ������������ ��� �������
            services.AddAutoMapper(typeof(MappingProfile).Assembly);

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                           .AddCookie(opt =>
                           {
                               opt.LoginPath = new PathString("/Account/Login");// ���� ������������ �� �������������, �� �� ����� ���������� �� ����� ����
                               opt.AccessDeniedPath = new PathString("/Account/Login");
                           });
            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("18+Content", policy =>
                    {
                        policy.Requirements.Add(new MinAgeRequirement(18));
                    });
            });
            services.AddSingleton<IAuthorizationHandler, MinAgeHandler>();

            #region Disscription AddNewtonsoftJson
            //��� �� ���������� ������������. ����� ������ �������� ������������� ������� �� ����� ������, ��������� 
            //� ������ � �������� ������������� ������, ������� �� �������� � �������� ������������ ������, ��� �����
            //���������� �� �����. ��� �� ��������� ������, ������� ��� ����������. �� ����������� ����� ����������
            //Microsoft.AspNetCore.Mvc.NewtonsoftJson � ���������� ����� �������.
            //������ ������ ��� � ������ Owner ��� ���������   public ICollection<Account>  Accounts { get; set; }
            //�������� ������� [Jsonignore] � ��� �������.
            //������ ����� ����� DTOUser � ������� �� �� ����� ������ �� �������.
            #endregion
            services.AddControllers().AddNewtonsoftJson(options =>
                       options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error"); //Repositories.Migrations
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();//��������� ������������� ������� � ������, ����� �������� ������ ���� �� ��������� �� ������ ���� ���-�� �������� � ����� ����� �������� � �������
            }
            app.UseHttpsRedirection();//������������� ������ �������� � Http �� Https
            app.UseStaticFiles();//���������� ������������ ����������� ����� �� wwwroot

            app.UseRouting();// �������� �� �������������

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
