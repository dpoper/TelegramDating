
namespace TelegramDating.Model
{
    public class Like : Entity
    {
        public virtual User User { get; set; }
        
        public virtual User CheckedUser { get; set; }

        public bool Liked { get; set; }

        public bool? Response { get; set; }

        public Like() { }

        public Like(User checkedUser, bool liked)
        {
            this.CheckedUser = checkedUser;
            this.Liked = liked;
        }
    }
}