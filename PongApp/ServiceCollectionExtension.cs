using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using PongApp.DataAccess;
using PongApp.DataAccess.Infrastructure.Interfaces;
using PongApp.Domain.Models.HealthChecks;

namespace PongApp
{
    public static class ServiceCollectionExtension
    {

        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(builder =>
            {
                builder.UseSqlite(configuration.GetConnectionString("DefaultConnection"),
                    assembly => assembly.MigrationsAssembly("PongApp.DataAccess"));
                //builder.LogTo(x => Debug.WriteLine(x));
            });

            services.AddScoped<IDbContext>(provider => provider.GetRequiredService<AppDbContext>());
            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PongApp", Version = "v1" });
            });
            return services;
        }

        public static IHealthChecksBuilder AddCustomHealthChecks(this IServiceCollection services)
        {
            var builder = services.AddHealthChecks();

            builder.AddCheck<DbContextCheck>("DbContextCheck");

            return builder;
        }

    }
}
