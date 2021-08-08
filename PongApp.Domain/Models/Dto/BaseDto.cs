using System;
using PongApp.Domain.Infrastructure.Interfaces.Models;

namespace PongApp.Domain.Models.Dto
{
    public class BaseDto : IBaseDto
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
