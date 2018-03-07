using System.Data.Linq.Mapping;

namespace TelegramDating
{
    [Table(Name = "Users")]
    public class User
    {
        [Column(Name = "Id", AutoSync = AutoSync.OnInsert, IsPrimaryKey = true, 
            IsDbGenerated = true, UpdateCheck = UpdateCheck.Never)]
        public int Id { get; private set; }
        
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
        [Column]
        public int State { get; set; }

        public User(string Name, int Age, bool Sex, int Photo)
        {
            // this.Id = Id;
            this.Name = Name;
            this.Age = Age;
            this.Sex = Sex;
            this.Photo = Photo;
            
            // Database.AddUser(this);
        }

        /// <summary>
        /// Constructor for LINQ mapping.
        /// </summary>
        public User() { }


    }
}
