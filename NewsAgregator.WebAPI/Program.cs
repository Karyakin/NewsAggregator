using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace NewsAgregator.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)//�� ����� ������������ ���� �� ������������ ������ � ����(Debug and trace �� �������)
               .WriteTo.Console(LogEventLevel.Debug)
               .WriteTo.File("logs/NewsAggregatorLog.txt", /*rollingInterval: RollingInterval.Day,*/ LogEventLevel.Information)
               .CreateLogger();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
             .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
