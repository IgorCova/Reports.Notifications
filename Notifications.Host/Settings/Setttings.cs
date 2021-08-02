using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;

namespace Notifications.Host
{
    public class ConnectionStringInfo
    {
        public string ConnectionString { get; set; }
        public string ProviderName { get; set; }
    }

    public class Settings
    {
        public static string ComponentName = "Notifications.Host";

        public ConnectionStringInfo ReportsConnectionString { get; }
        private readonly IConfiguration configuration;

        public Settings(IConfiguration configuration)
        {
            this.configuration = configuration;

            ReportsConnectionString = new ConnectionStringInfo
            {
                ConnectionString = configuration.GetConnectionString("db.reports"),
                ProviderName = configuration.GetSection("providerNames").GetValue<string>("db.reports")
            };
        }

        public string Get(string key)
        {
            return configuration.GetValue<string>(key);
        }
    }
}