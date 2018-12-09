using System.Data.Entity.Migrations;
using TelegramDating.Database;

namespace TelegramDating.Database.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<UserContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.ContextKey = "TelegramDating.Database.UserContext";
        }
    }
}
