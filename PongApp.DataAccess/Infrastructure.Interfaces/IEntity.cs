using System;

namespace PongApp.DataAccess.Infrastructure.Interfaces
{
    public interface IEntity
    {
        Guid Id { get; set; }
        DateTime CreateDate { get; set; }
    }
}
