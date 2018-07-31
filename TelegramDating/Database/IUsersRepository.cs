using TelegramDating.Model;

namespace TelegramDating.Database
{
    interface IUserRepository : IRepository<User>
    {
        bool Contains(long id);
        bool Contains(string username);
    }
}
