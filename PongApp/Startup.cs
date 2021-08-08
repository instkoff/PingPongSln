using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper.EquivalencyExpression;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using PongApp.Domain.Infrastructure.Interfaces.Services;
using PongApp.Domain.Services;
using PongApp.Domain.Utils;
using PongApp.Utils;

namespace PongApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwagger();
            services.AddDatabase(Configuration);
            services.AddAutoMapper(config => config.AddCollectionMappers(),
                typeof(AutoMapperProfile));
            services.AddCustomHealthChecks();
            services.AddHealthChecksUI(s=>s.AddHealthCheckEndpoint("endpoint1", "https://localhost:5001/health"))
                .AddInMemoryStorage();
            services.AddScoped<IMessageService, MessageService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "PongApp");
                x.RoutePrefix = "swagger";
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecksUI();
                endpoints.MapHealthChecks("/health", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
            });
        }
    }
}
