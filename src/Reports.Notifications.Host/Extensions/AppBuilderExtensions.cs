using Microsoft.AspNetCore.Builder;
using Reports.Notifications.Host.Middleware;

namespace Reports.Notifications.Host.Extensions
{
    public static class AppBuilderExtensions
    {
        public static void UseGlobalExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalExceptionHandler>();
        }
    }
}