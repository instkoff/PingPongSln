using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PingApp.Models.Settings;

namespace PingApp
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var configuration = BuildConfiguration(args);
            var serviceProvider = ConfigureServices(configuration);
            var app = serviceProvider.GetRequiredService<MainApp>();
            await app.Execute();
        }

        private static IServiceProvider ConfigureServices(IConfiguration config)
        {
            var services = new ServiceCollection();
            services.Configure<AppSettings>(config.GetSection(nameof(AppSettings)));
            services.AddSingleton<MainApp>();
            services.AddHttpClientWithSettings(config);

            return services.BuildServiceProvider();
        }

        private static IConfiguration BuildConfiguration(string[] args)
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", false);
            return builder.Build();
        }
    }
}