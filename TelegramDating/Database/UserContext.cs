using System.Data.Entity;
using TelegramDating.Model;

namespace TelegramDating.Database
{
    sealed class UserContext : DbContext
    {
        public UserContext() : base("DBConnection")
        {
        }

        private DbSet<User> Users { get; set; }
    }
}
