using System.Data.Entity.Migrations;

namespace TelegramDating.Database.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<UserContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            this.ContextKey = "TelegramDating.Database.UserContext";
        }
    }
}
