using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongApp.Domain.Infrastructure.Interfaces.Models
{
    public interface IBaseDto
    {
        Guid Id { get; set; }
        DateTime CreateDate { get; set; }
    }
}
