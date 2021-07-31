using System;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Module.AspNetCore;
using Module.Logging.Core;
using Module.Logging.NLog;

namespace Notifications.Host
{
    public class Program
    {
        public static void Main(string[] args)
        {
            NLogBootstrapper.ConfigureFromEnvironment(Settings.Settings.ComponentName);
            LoggingManager.Configure(c => c.SetComponentName(Settings.Settings.ComponentName).UseNLog());
            Bootstrapper.Run<Startup>(new StartupParams
            {
                Url = "http://0.0.0.0:9229",
                Args = args,
                WebHostBuilderInitializer = builder =>
                    builder.UseContentRoot(Directory.GetCurrentDirectory()),
                HostBuilderInitializer = builder =>
                    builder.ConfigureWebHostDefaults(webBuilder => { })
            });
        }
    }
}