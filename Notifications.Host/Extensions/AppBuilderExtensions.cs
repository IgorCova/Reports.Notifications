using Microsoft.AspNetCore.Builder;
using Notifications.Host.Middleware;

namespace Notifications.Host.Extensions
{
    public static class AppBuilderExtensions
    {
        public static void UseGlobalExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalExceptionHandler>();
        }
    }
}