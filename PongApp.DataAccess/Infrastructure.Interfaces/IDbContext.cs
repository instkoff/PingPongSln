using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PongApp.DataAccess.Entities;

namespace PongApp.DataAccess.Infrastructure.Interfaces
{
    public interface IDbContext
    {
        DbSet<UserEntity> Users { get; set; }

        DbSet<MessageEntity> Messages { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        Task<bool> CanConnectToDatabaseAsync(CancellationToken cancellationToken);
    }
}