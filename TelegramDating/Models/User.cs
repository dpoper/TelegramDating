using System;
using System.Data.Linq.Mapping;
using System.Linq;

namespace TelegramDating
{
    [Table(Name = "Users")]
    public class User
    {
        /// <summary>
        /// Pass chat_id here.
        /// </summary>
        [Column(Name = "Id", IsPrimaryKey = true, IsDbGenerated = false)]
        public long Id { get; set; }

        /// <summary>
        /// Name that will be displayed to the person who found this user.
        /// </summary>
        [Column]
        public string Name { get; set; }

        /// <summary>
        /// E.g durov (without '@' symbol at the beginning)
        /// </summary>
        [Column]
        public string Username { get; set; }

        /// <summary>
        /// Current user's current age.
        /// </summary>
        [Column]
        public int Age { get; set; }

        /// <summary>
        /// Current user's sex.
        /// 
        /// Male   = True
        /// Female = False
        /// </summary>
        public bool Sex { get; set; }

        /// <summary>
        /// Country name.
        /// </summary>
        [Column]
        public string Country { get; set; }

        /// <summary>
        /// City name.
        /// </summary>
        [Column]
        public string City { get; set; }

        /// <summary>
        /// Photo id from telegram server.
        /// Will be used as profile picture of current user.
        /// </summary>
        [Column]
        public int Photo { get; set; }

        /// <summary>
        /// Current user's preferences
        ///     (Sex, City)
        /// </summary>
        [Column]
        public int SearchOptions { get; set; }

        /// <summary>
        /// Preferred age. '-' symbol as delimiter.
        /// E.g "18-23"
        /// </summary>
        [Column]
        public string SearchAge { get; set; }
        
        /// <summary>
        /// Current user's state. 
        ///  
        /// E.g creating profile/searching for people/...
        /// </summary>
        [Column(Name = "State")]
        public int State { get; set; }


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

            State = (int)TelegramDating.State.Create.Name;
        }

    }
}
