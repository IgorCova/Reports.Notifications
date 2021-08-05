using LightInject;
using System;
using Module.Reporting.Providers.Digest;
using Module.Reporting.Providers.Contracts;
using Module.Logging.Core;
using Module.Logging.Core.Loggers;
using Reports.Notifications.Host.Jobs;

namespace Reports.Notifications.Host.Configuration
{
    public static class ExecutionRegistry
    {
        public static IServiceRegistry RegisterExecutionTypes(this IServiceRegistry container)
        {
            container.RegisterSingleton<IDigestProvider>(
             factory => new DigestProvider(factory.GetInstance<Settings>().ReportsConnectionString, new TimeSpan(0, 1, 0), 60));

            container.RegisterSingleton<IDigestService>(
                factory => new DigestService(factory.GetInstance<IDigestProvider>()));

            container.Register(typeof(ILogger<>), typeof(TypedLogger<>));

            container.RegisterSingleton<DailyDigestJob>();
            container.RegisterJobBootstrapper();

            return container;
        }
    }
}
