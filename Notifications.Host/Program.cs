using System.ComponentModel.Design;
using LightInject.Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;

namespace Notifications.Host
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
                .UseUrls(new[] { "http://0.0.0.0:9229" })
                .UseStartup<Startup>();

        // public static void Main(string[] args)
        // {
        //     NLogBootstrapper.ConfigureFromEnvironment(Settings.ComponentName);
        //     LoggingManager.Configure(c => c.SetComponentName(Settings.ComponentName).UseNLog());

        //     var startupParams = new StartupParams
        //     {
        //         Urls = new[] { "http://0.0.0.0:9229" },
        //         Args = args,
        //         HostBuilderInitializer = builder =>
        //             builder
        //                 .UseLightInject()
        //                 .UseStartup<Startup>()
        //     };
        //     Bootstrapper.Run<Startup>(startupParams);
        // }
    }
}