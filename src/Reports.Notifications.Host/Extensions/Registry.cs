using LightInject;
using Module.Logging.Core;
using Reports.Notifications.Host.Jobs;

namespace Reports.Notifications.Host
{
    public static class Registry
    {
        public static IServiceRegistry RegisterJobBootstrapper(this IServiceRegistry container)
        {
            container.RegisterSingleton(factory => new JobBootstrapper(
                factory.GetInstance<DailyDigestJob>(),
                factory.GetInstance<ILoggerFactory>(),
                factory.GetInstance<ISettings>().SchedulerRabbitMq,
                factory.GetInstance<ISettings>().ReportsConnectionString)
            );
            return container;
        }
    }
}