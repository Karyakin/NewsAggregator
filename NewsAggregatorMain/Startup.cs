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

/*PS C:\Users\d.karyakin\Desktop\NewsAggregator\RepositoryBase> dotnet ef --startup-project ../NewsAggregatorMain/ migration
s add Initial*/
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
            services.AddControllersWithViews();
            services.AddDbContext<NewsDataContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("SqlConnectionStr"));
            });
            services.AddScoped<IUnitOfWork, RepositoryUnitOfWork>();
            services.AddScoped<INewsService, NewsService>(); 
            services.AddScoped<ICategoryService, CategoryService>(); 
            services.AddScoped<IRssSourceService, RssSourceService>(); 

            services.AddAutoMapper(typeof(MappingProfile).Assembly);


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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

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
