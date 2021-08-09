using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongApp.Domain.Models.Request
{
    public class AddMessageRequest : BaseRequest
    {
        public string Message { get; set; }
    }
}
