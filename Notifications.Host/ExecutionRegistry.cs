using LightInject;
using System;
using Module.Reporting.Providers.Digest;
using Module.Reporting.Providers.Contracts;

namespace Notifications.Host.Configuration
{
    public static class ExecutionRegistry
    {
        public static IServiceRegistry RegisterExecutionTypes(this IServiceRegistry container)
        {
            container.RegisterSingleton<IDigestProvider>(
             factory => new DigestProvider(factory.GetInstance<Settings>().ReportsConnectionString.ConnectionString, new TimeSpan(0, 1, 0), 60));

            container.RegisterSingleton<IDigestService>(
                factory => new DigestService(factory.GetInstance<IDigestProvider>()));

            return container;
        }
    }
}
