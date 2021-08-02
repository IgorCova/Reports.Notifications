using Microsoft.EntityFrameworkCore;

namespace Notifications.Host
{
    public class DefaultDbContext : DbContext
    {
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options)
            : base(options) { }
    }
}