using System.Data.Entity;
using TelegramDating.Model;

namespace TelegramDating.Database
{
    public sealed class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public UserContext() : base("DBConnection") { }
    }
}
