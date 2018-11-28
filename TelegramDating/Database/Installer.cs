using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using TelegramDating.Model.Commands;

namespace TelegramDating.Database
{
    public class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component
                .For<UserContext>()
                .ImplementedBy<UserContext>());
        }
    }
}