using LightInject;
using Module.Amqp;
using Module.Amqp.LightInject;
using Module.Amqp.Topology;
using Module.Logging.Core;
using Module.Tracing.Contract.NullTracer;

namespace Reports.Notifications.Host.Configuration
{
    public class ContainerCompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry container)
        {
            container.RegisterExecutionTypes();

            container.RegisterSingleton<IMessageBusFactory>(c =>
              new MessageBusFactory(c.GetInstance<ILoggerFactory>(), NullTracer.Instance));

            container.RegisterMessageBus(
                c =>
                {
                    var settings = c.GetInstance<ISettings>();
                    return $"{settings.SchedulerRabbitMq};prefetchcount={settings.PrefetchCount}";
                },
                c => new ServiceTopology("replicator")
            );
        }
    }
}