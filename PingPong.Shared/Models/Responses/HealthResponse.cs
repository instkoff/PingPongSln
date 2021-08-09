using System.Collections.Generic;

namespace PingPong.Shared.Models.Responses
{
    public class HealthResponse : BaseResponse
    {
        public string Status { get; set; }

        public string TotalDuration{ get; set; }

        public Dictionary<string, HealthEntry> Entries { get; set; }
    }

    public class HealthEntry
    {
        public string Description { get; set; }

        public string Status { get; set; }
    }
}
