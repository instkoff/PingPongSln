using System;

namespace PingPong.Shared.Models.Dto
{
    public class BaseDto
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
