using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Recademy.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            ILogger logger = new LoggerConfiguration()
                .WriteTo.File("RecademyApi.log")
                .CreateLogger();

            return Host.CreateDefaultBuilder(args)
                .UseSerilog(logger)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }
    }
}