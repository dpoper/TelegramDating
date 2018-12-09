using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TelegramDating.Model
{
    public sealed class Like
    {
        [Key]
        [Column(Order = 0)]
        public long Id { get; set; }

        public User User { get; set; }

        public User CheckedUser { get; set; }

        public bool Liked { get; set; }

        public Like() { }

        public Like(User checkedUser, bool liked)
        {
            this.CheckedUser = checkedUser;
            this.Liked = liked;
        }
    }
}