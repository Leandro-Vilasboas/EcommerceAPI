using EcommerceAPI.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

                // Cria o host
                var host = CreateHostBuilder(args).Build();

                // Executa as migrations automaticamente ao iniciar
                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    var context = services.GetRequiredService<AppDbContext>();
                    context.Database.Migrate(); // Aplica as migrations no banco do Docker
                }

                host.Run(); // Inicia a aplicação
                //CreateHostBuilder(args).Build().Run();
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
