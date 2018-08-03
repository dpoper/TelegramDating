using System;
using System.Linq;
using TelegramDating.Model;

namespace TelegramDating.Database
{
    internal sealed class UserRepository : Repository<User>, IUserRepository, IDisposable
    {
        private static UserRepository _userRepo;

        private UserRepository(UserContext userContext) : base(userContext)
        {
        }

        public static UserRepository Initialize()
        {
            if (_userRepo == null)
                _userRepo = new UserRepository(new UserContext());

            return _userRepo;
        }

        public bool Contains(long userId)
        {
            var found = this.Find(user => user.UserId == userId);

            Console.WriteLine("Fnd " + found.Count());

            return found.Count() != 0;
        }

        public override User Get(long userId)
        {
            return Context.Set<User>().SingleOrDefault(user => user.UserId == userId);
        }

        public bool Contains(string username)
        {
            throw new NotImplementedException();
        }

        public int Submit() => Context.SaveChanges();

        public void Dispose() => Context.Dispose();
    }
}
