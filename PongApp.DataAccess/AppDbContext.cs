using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PongApp.DataAccess.Entities;
using PongApp.DataAccess.Infrastructure.Interfaces;

namespace PongApp.DataAccess
{
    public class AppDbContext : DbContext, IDbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<UserEntity> Users { get; set; }

        public DbSet<MessageEntity> Messages { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().HasIndex(u => u.Name).IsUnique();
        }

        public async Task<bool> CanConnectToDatabaseAsync(CancellationToken cancellationToken)
        {
            return await Database.CanConnectAsync(cancellationToken);
        }
    }
}
