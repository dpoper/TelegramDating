using Castle.Windsor;

namespace TelegramDating
{
    public static class Container
    {
        public static readonly IWindsorContainer Current = new WindsorContainer();
    }
}
