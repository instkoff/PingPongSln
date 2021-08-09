using System;
using PongApp.DataAccess.Infrastructure.Interfaces;

namespace PongApp.DataAccess.Entities
{
    public class BaseEntity : IEntity
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}