using Microsoft.EntityFrameworkCore;

namespace Reports.Notifications.Host.Configuration
{
    public class DefaultDbContext : DbContext
    {
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options)
            : base(options) { }
    }
}