using System;

namespace TelegramDating
{
    public class User
    {
        public string Name   { get; }
        public int    Age    { get; }
        public Gender Gender { get; }
        public Photo  Photo  { get; }

        public User(string Name, int Age, Gender Gender, Photo Photo)
        {
            this.Name   = Name;
            this.Age    = Age;
            this.Gender = Gender;
            this.Photo  = Photo;

            Database.AddUser(this);
        }


    }
    
    public enum Gender { Male, Female }
}
