using System.Linq;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace TelegramDating.Bot
{
    public class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var botWorker = new BotWorker(container.Resolve<Database.UserContext>());

            botWorker.Instance.OnMessage += botWorker.HandleMessage;
            botWorker.Instance.OnCallbackQuery += botWorker.HandleCallbackQuery;

            var userContext = container.Resolve<Database.UserContext>();

            botWorker.AvailableSlashCommandList.ToList().ForEach(sc => {
                sc.BotWorker = botWorker;
                sc.UserContext = userContext;
            });

            botWorker.ProfileCreatingAskActions.ToList().ForEach(pcaa => pcaa.BotWorker = botWorker);

            container.Register(Component.For<BotWorker>()
                                        .Instance(botWorker));

            botWorker.Instance.StartReceiving();
        }
    }
}