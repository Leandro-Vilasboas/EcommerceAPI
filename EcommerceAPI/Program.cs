using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;

namespace EcommerceAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IConfigurationRoot configuration = GetConfiguration();
            ConfiguraLog(configuration);    

            try
            {
                Log.Information("Iniciando a API");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Erro de execução");
                throw;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static void ConfiguraLog(IConfigurationRoot configuration)
        {
            Log.Logger = new LoggerConfiguration()
                            /*.Enrich.FromLogContext()
                            .MinimumLevel.Warning()
                            //.WriteTo.File("log.txt", fileSizeLimitBytes: 100000, rollOnFileSizeLimit:true, rollingInterval: RollingInterval.Day)
                            .WriteTo.Async(p => p.File("log.txt", fileSizeLimitBytes: 100000, rollOnFileSizeLimit: true, rollingInterval: RollingInterval.Day))*/
                            .ReadFrom.Configuration(configuration)
                            .CreateLogger();
        }

        private static IConfigurationRoot GetConfiguration()
        {
            string ambiente = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{ambiente}.json")
                .Build();
            return configuration;
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
