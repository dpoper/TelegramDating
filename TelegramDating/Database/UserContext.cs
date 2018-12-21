using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TelegramDating.Model;

namespace TelegramDating.Database
{
    public sealed class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public UserContext() : base("TelegramDating")
        {
            System.Data.Entity.Database.SetInitializer(new MigrateDatabaseToLatestVersion<UserContext, Migrations.Configuration>());
        }

        public User GetByUserId(long userId) => this.Users.SingleOrDefault(u => u.UserId == userId && !u.DeletedAt.HasValue);

        public ICollection<Like> LoadLikes(long id) => this.Users.Include(u => u.Likes).Include(u => u.GotLikes)
                                                        .SingleOrDefault(u => u.Id == id && !u.DeletedAt.HasValue).Likes;

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Like>().HasRequired(x => x.User).WithMany(x => x.Likes).WillCascadeOnDelete(false);
            modelBuilder.Entity<Like>().HasRequired(x => x.CheckedUser).WithMany(x => x.GotLikes).WillCascadeOnDelete(false);
        }
    }
}
