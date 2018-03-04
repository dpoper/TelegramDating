using System;
using System.Linq;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Data.Linq.Mapping;

namespace TelegramDating
{
    public static class Database
    {
        // private static string connect = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True";

        private static string connect = TelegramDating.Properties.Settings.Default.DatabaseConnectionString;
        
        private static DataContext DB = new DataContext(connect);
        
        private static Table<User> Users { get; } = DB.GetTable<User>();

        // private static 

        public static void Test()
        {

            var user = (from u in Users select u).First();

            Console.WriteLine(user.Name);
            
        }

        public static void AddTest()
        {
            var user = new User(1, "Хуй", 10000, 1, 1488);

            Users.InsertOnSubmit(user);
        }

        /// <summary>
        /// Add user to database.
        /// </summary>
        /// <param name="User"></param>
        public static void AddUser(User User)
        {
            throw new NotImplementedException();
        }

        // Пока не придумал, по какому па
        public static bool ContainsUser(User User)
        {
            throw new NotImplementedException();
        }

        public static bool ContainsUser(long UserId) {

            //throw new NotImplementedException();
            return false;
        }
    }
}