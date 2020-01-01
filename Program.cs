using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

namespace StudentApp
{
#pragma warning disable CS1591

    public class Program
    {
        public static void Main(string[] args)
        {
            // Init Serilog configuration
            var configuration = new ConfigurationBuilder()
              .AddJsonFile("appsettings.logs.json")
              .Build();

            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();

            try
            {
                Log.Information("Starting web host");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureAppConfiguration((hostContext, config) =>
                {
                    var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                    var settings = env == null ? "appsettings.json" : $"appsettings.{env}.json";
                    config.AddJsonFile(settings, optional: true, reloadOnChange: true);
                })
                .UseSerilog();
    }

#pragma warning restore CS1591
}