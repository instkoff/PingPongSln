using System;
using System.Net;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PingApp.Models.Settings;
using PingApp.Utils;

namespace PingApp
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddHttpClientWithSettings(this IServiceCollection services,
            IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            var settings = new AppSettings();
            configuration.Bind(nameof(AppSettings), settings);

            if (!settings.ProxySettings.Enabled)
            {
                services.AddHttpClient<PongAppClient>(cfg => cfg.BaseAddress = new Uri(settings.ApiEndpoint));
                return services;
            }


            services.AddHttpClient<PongAppClient>(cfg => cfg.BaseAddress = new Uri(settings.ApiEndpoint))
                .ConfigurePrimaryHttpMessageHandler(() =>
                {
                    if (settings.ProxySettings.AuthRequired)
                        return new HttpClientHandler
                        {
                            Proxy = new WebProxy(new Uri(settings.ProxySettings.IpAddress), true, null,
                                new NetworkCredential(settings.ProxySettings.Login, settings.ProxySettings.Password))
                        };

                    return new HttpClientHandler
                    {
                        Proxy = new WebProxy(new Uri(settings.ProxySettings.IpAddress), true, null)
                    };
                });


            return services;
        }
    }
}