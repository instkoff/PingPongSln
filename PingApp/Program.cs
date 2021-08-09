using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PingApp.Models.Settings;

namespace PingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = BuildConfiguration(args);
            var serviceProvider = ConfigureServices(configuration);
            var app = serviceProvider.GetRequiredService<MainApp>();
            app.Execute();
        }

        private static IServiceProvider ConfigureServices(IConfiguration config)
        {
            return new ServiceCollection()
                .Configure<AppSettings>(config.GetSection(nameof(AppSettings)))
                .AddSingleton<MainApp>()
                .AddHttpClient()
                .BuildServiceProvider();
        }

        private static IConfiguration BuildConfiguration(string[] args)
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional: false);
            return builder.Build();
        }
    }
}
