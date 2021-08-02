using Microsoft.Extensions.Configuration;

namespace Notifications.DataProvider
{
    public class Settings
    {
        public string ReportsConnectionString { get; }

        public Settings(IConfiguration configuration)
        {
            ReportsConnectionString = configuration.GetConnectionString("db.reports");
        }
    }
}