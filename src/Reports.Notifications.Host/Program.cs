using System.ComponentModel.Design;
using LightInject.Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;

namespace Reports.Notifications.Host
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseLightInject()
                // .UseSetting("https_port", "5001")
                .UseUrls(new[] { "http://host.docker.internal:9229" })
                .UseStartup<Startup>();
    }
}