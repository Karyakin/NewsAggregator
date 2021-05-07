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
using Contracts.ParseInterface;
using NewsAggregatorMain.Filters;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Contracts.RepositoryInterfaces;
using Repositories.NewsRep;
using Repositories.CountryRepo;

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
            services.AddDbContext<NewsDataContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("SqlConnectionStr"),
                    x => x.MigrationsAssembly(typeof(NewsDataContext).Assembly.FullName));
            });

            services.AddControllersWithViews();
            services.AddScoped<IUnitOfWork, RepositoryUnitOfWork>();
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<IRssSourceService, RssSourceService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IRoleService, RoleService>();


            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();


            services.AddScoped<TutByParser>(); //внедрение без привязки к родитель(альтернатива)
            services.AddScoped<OnlinerParser>();//внедрение без привязки к родитель(альтернатива)
            //services.AddScoped<IOnlinerParser, OnlinerParser>();
            //services.AddScoped<ITutByParser, TutByParser>(); 

            services.AddControllersWithViews()
                .AddMvcOptions(opt =>
                {
                    opt.Filters.Add(new ChromFilterAttribute());
                    opt.Filters.Add(new CustomExceptionFilterAttribite());
                });// подключаем много фильтров


            services.AddScoped<CheckDataFilterAttribute>();// внедрение зависимостей для фильтра
                                                           // services.AddScoped<CustomExceptionFilterAttribite>();// внедрение зависимостей для фильтра
            services.AddAutoMapper(typeof(MappingProfile).Assembly);

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                           .AddCookie(opt =>
                           {
                               opt.LoginPath = new PathString("/Account/Login");// если пользователь не авторизирован, то он будет переброшен по этому пути
                               opt.AccessDeniedPath = new PathString("/Account/Login");
                           });



            #region Disscription AddNewtonsoftJson
            //тут мы прекращаем зацикливание. Когда ентити начинает сериализовать аккаунт он видит оунера, переходит 
            //в оунера и начинает сериализовать оунера, доходит до аккаунта и начинает серилизовать аккаун, где опять
            //натыкается на унера. Тут мы добавляем сервис, который это прекращает. Он добавляется через нунгепакет
            //Microsoft.AspNetCore.Mvc.NewtonsoftJson и внедряется через сервисы.
            //второй способ это в классе Owner над свойством   public ICollection<Account>  Accounts { get; set; }
            //добавить атрибут [Jsonignore] и все решится.
            //третий спосо через DTOUser в котором мы не будет ссылки на аккаунт.
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
                app.UseHsts();//проверяет достоверность системы и следит, чтобы обменные пакеты были не подменены на случай если кто-то влинится в канал между клиентом и серверо
            }
            app.UseHttpsRedirection();//автоматически делает редирект с Http на Https
            app.UseStaticFiles();//позволяеть использовать статические файлы из wwwroot

            app.UseRouting();// отвечает за маршрутизацию

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
