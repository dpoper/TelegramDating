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

        public ICollection<Like> LoadLikes(User userWithNoLikesLoaded)
        {
            return this.Users.Include(u => u.Likes)
                               .SingleOrDefault(u => u.Id == userWithNoLikesLoaded.Id && !u.DeletedAt.HasValue).Likes;
        }

        public ICollection<Like> LoadGotLikes(User userWithNoGotLikesLoaded)
        {
            return this.Users.Include(u => u.GotLikes)
                             .SingleOrDefault(u => u.Id == userWithNoGotLikesLoaded.Id && !u.DeletedAt.HasValue).GotLikes;
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany(x => x.Likes)
                                       .WithRequired(x => x.User).WillCascadeOnDelete(false);

            modelBuilder.Entity<User>().HasMany(x => x.GotLikes)
                                       .WithRequired(x => x.CheckedUser).WillCascadeOnDelete(false);
        }
    }
}
