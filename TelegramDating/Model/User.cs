using System;
using System.Threading.Tasks;
using TelegramDating.Model.Enums;
using TelegramDating.Model.StateMachine;

namespace TelegramDating.Model
{
    public class User
    {
        /// <summary>
        /// Pass chat_id here.
        /// </summary>
        public long Id { get; set; }

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
        /// Current user's state. 
        ///  
        /// E.g creating profile/searching for people/...
        /// </summary>
        public State State { get; set; }

        /// <summary>
        /// Constructor for LINQ mapping.
        /// </summary>
        public User() { }

        /// <summary>
        /// Constructor for adding users into database.
        /// </summary>
        /// <param name="Id"></param>
        public User(long Id, string Username)
        {
            this.Id = Id;
            this.Username = Username;

            State = new StateHello();
        }

        public async Task HandleState(EventArgs msgOrCallback)
        {
            await State.Handle(this, msgOrCallback);
        }
    }
}
