using System;
using System.Data.Linq.Mapping;

namespace TelegramDating
{
    [Table(Name = "Users")]
    public class User
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; }

        [Column]
        public string Name { get; }

        [Column]
        public int Age { get; }

        // [Column]
        // public Sex    Sex    { get; }

        [Column]
        public int Sex { get; }

        [Column]
        public int Photo { get; }

        // state
        // city

        // whosex
        // whoage

        public User(int Id, string Name, int Age, int Sex, int Photo)
        {
            this.Id = Id;
            this.Name = Name;
            this.Age = Age;
            this.Sex = Sex;
            this.Photo = Photo;

            // Database.AddUser(this);
        }


    }


    public enum Sex { Female, Male }
}
