using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using Serilog;

namespace PongApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateBootstrapLogger();

            try
            {
                CreateHostBuilder(args).Build().Run();

            }
            catch (Exception e)
            {
                Log.Fatal(e, "An unhandled exception occured during startup");
            }
            finally
            {
                Log.CloseAndFlush();
            }


        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
                .UseSerilog((context, services, configuration) =>
                    configuration.ReadFrom.Configuration(context.Configuration));
        }
    }
}
