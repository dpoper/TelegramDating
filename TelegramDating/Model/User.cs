using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using TelegramDating.Model.Enums;

namespace TelegramDating.Model
{
    public sealed class User
    {
        [Key]
        [Column(Order = 0)]
        public long Id { get; set; }

        /// <summary>
        /// Pass chat_id here.
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// Name that will be displayed to the person who found this user.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// E.g durov (without '@' symbol at the beginning)
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Current user's current age.
        /// </summary>
        public int Age { get; set; }

        /// Current user's sex.
        /// 
        /// Male   = True
        /// Female = False
        public bool Sex { get; set; }

        /// <summary>
        /// Country name.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// City name.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Photo id from telegram server.
        /// Will be used as profile picture of current user.
        /// </summary>
        public string PictureId { get; set; }

        /// <summary>
        /// Current user's preferences
        ///     (Sex, City)
        /// </summary>
        public SearchOptions.Sex SearchSex { get; set; }

        /// <summary>
        /// Current user's state id.
        /// </summary>
        public int ChatActionId { get; set; } = 0;

        /// <summary>
        /// Constructor for DB mapping.
        /// </summary>
        public User() { }

        /// <summary>
        /// Constructor for adding users into database.
        /// </summary>
        public User(long userId, string username)
        {
            this.UserId = userId;
            this.Username = username;
        }

        public async Task HandleAction(EventArgs msgOrCallback)
        {
            await BotWorker.FindAction(ChatActionId).Execute(this, msgOrCallback);
        }
    }
}
