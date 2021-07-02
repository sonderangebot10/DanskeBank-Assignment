using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace DanskeBank.CompaniesApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

            Log.Information("Companies API started...");
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseIISIntegration();
                    webBuilder.UseStartup<Startup>();
                    webBuilder.ConfigureAppConfiguration((builderContext, config) =>
                    {
                        var env = builderContext.HostingEnvironment;
                        config.AddJsonFile("appsettings.json", optional: false);
                        config.AddJsonFile(
                            $"appsettings.{env.EnvironmentName}.json",
                            optional: true);

                        config.AddJsonFile("autofac.json", optional: false);
                        config.AddJsonFile(
                            $"autofac.{env.EnvironmentName}.json",
                            optional: true);
                    })
                    .UseSerilog()
                    .ConfigureServices(services => services.AddAutofac());
                });
    }
}
