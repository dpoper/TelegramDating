
namespace TelegramDating.Model
{
    public sealed class Like : Entity
    {
        public User User { get; set; }

        public User CheckedUser { get; set; }

        public bool Liked { get; set; }

        public bool? Answer { get; set; }

        public Like() { }

        public Like(User checkedUser, bool liked)
        {
            this.CheckedUser = checkedUser;
            this.Liked = liked;
        }
    }
}