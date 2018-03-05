using System;
using System.Data.Linq.Mapping;

namespace TelegramDating
{
    [Table(Name = "Users")]
    public class User
    {
        [Column(Name = "Id", AutoSync = AutoSync.OnInsert, IsPrimaryKey = true, 
            IsDbGenerated = true, UpdateCheck = UpdateCheck.Never)]
        public int Id { get; set; }

        [Column]
        public string Name { get; set; }

        [Column]
        public int Age { get; set; }
        
        [Column(DbType = "bit")]
        public byte Sex { get; set; }

        [Column]
        public int Photo { get; set; }

        // state
        // city

        // whosex
        // whoage
        
        
        public User(int Id, string Name, int Age, byte Sex, int Photo)
        {
            this.Id = Id;
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
