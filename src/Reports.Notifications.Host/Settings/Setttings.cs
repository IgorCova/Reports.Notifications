using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;

namespace Reports.Notifications.Host
{
    public class Settings : ISettings
    {
        public static string ComponentName = "Reports.Notifications.Host";
        public string ReportsConnectionString { get; }
        public string HangfireConnectionString { get; }
        public string PostgresConnectionString { get; }
        public string SchedulerRabbitMq { get; }
        public int PrefetchCount { get; }

        private readonly IConfiguration _configuration;
        
        public Settings(IConfiguration configuration)
        {
            this._configuration = configuration;
            this.ReportsConnectionString = configuration.GetConnectionString("db.reports");
            this.PostgresConnectionString = configuration.GetConnectionString("db.postgres");
            this.HangfireConnectionString = configuration.GetConnectionString("db.postgres.hangfire");
            this.SchedulerRabbitMq = Get("SchedulerRabbitMq");
            this.PrefetchCount = 10;
        }

        public string Get(string key)
        {
            return _configuration.GetValue<string>(key);
        }
    }
}