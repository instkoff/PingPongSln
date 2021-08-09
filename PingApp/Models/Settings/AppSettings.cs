namespace PingApp.Models.Settings
{
    public class AppSettings
    {
        public string Username { get; set; }

        public string ApiEndpoint { get; set; }

        public ProxySettings ProxySettings { get; set; }
    }

    public class ProxySettings
    {
        public bool Enabled { get; set; }

        public string IpAddress { get; set; }
    }
}
