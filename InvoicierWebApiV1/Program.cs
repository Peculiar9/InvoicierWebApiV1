using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Security.Authentication;

namespace InvoicierWebApiV1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder() 
                .AddJsonFile("appsettings.json")
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            try
            {
                Log.Information("Application is starting up.");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application failed to start correctly");

                throw;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
         Host.CreateDefaultBuilder(args)
            .UseSerilog()
             .ConfigureWebHostDefaults(webBuilder =>
             {
                 webBuilder.ConfigureKestrel(serverOptions =>
                 {
                     serverOptions.Limits.MinRequestBodyDataRate = new MinDataRate(100, TimeSpan.FromSeconds(10));
                     serverOptions.Limits.MinResponseDataRate = new MinDataRate(100, TimeSpan.FromSeconds(10));
                     serverOptions.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(2);
                     serverOptions.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(1);
                     serverOptions.ConfigureHttpsDefaults(listenOptions =>
                     {
                         listenOptions.SslProtocols = SslProtocols.Tls12;
                     });
                 })
                 .UseStartup<Startup>();
             });
    }
}
