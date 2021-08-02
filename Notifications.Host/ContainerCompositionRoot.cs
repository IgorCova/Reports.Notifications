using LightInject;
using Notifications.Host.Configuration;

namespace Notifications.Host
{
    public class ContainerCompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry container)
        {
            container.RegisterExecutionTypes();
        }
    }
}