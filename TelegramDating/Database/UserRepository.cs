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

        public bool Contains(long id)
        {
            return this.Find(user => user.Id == id).Count() > 0;
        }

        public bool Contains(string username)
        {
            throw new NotImplementedException();
        }

        public int Submit() => Context.SaveChanges();

        public void Dispose() => Context.Dispose();
    }
}
