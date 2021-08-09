using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using PongApp.DataAccess.Infrastructure.Interfaces;

namespace PongApp.Domain.Models.HealthChecks
{
    public class DbContextCheck : IHealthCheck
    {
        private readonly IDbContext _dbContext;

        public DbContextCheck(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
            CancellationToken cancellationToken = new())
        {
            try
            {
                if (await _dbContext.CanConnectToDatabaseAsync(cancellationToken))
                    return HealthCheckResult.Healthy("DbContextCheck passed.");

                return HealthCheckResult.Unhealthy("DbContextCheck could not connect to database.");
            }
            catch (Exception e)
            {
                return HealthCheckResult.Unhealthy("Error when trying to check DbContext. ", e);
            }
        }
    }
}