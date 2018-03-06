using System;
using System.Linq;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace TelegramDating
{
    public static class Database
    {
        public static string ConnectLink { get; } = Properties.Settings.Default.DatabaseConnection;

        private static DataContext DB = new DataContext(ConnectLink);
        private static Table<User> UsersTable = DB.GetTable<User>();
        // private static Table<User> UsersTable = DB.GetTable<User>();

        public static void Show()
        {
            try
            {
                DB = new DataContext(ConnectLink);
                Table<User> UsersTable = DB.GetTable<User>();

                var s = string.Join("\n", UsersTable.Select(u => u.Name).ToList());

                Console.WriteLine(s);
            } catch (Exception ex) { Console.WriteLine(ex.Message); }

        }

        /// <summary>
        /// Add user to database.
        /// </summary>
        /// <param name="User"></param>
        public static void AddUser(User user)
        {
            try
            {
                
                UsersTable.InsertOnSubmit(user);
                UsersTable.Context.SubmitChanges();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }


        }
        
        public static bool ContainsUser(User User)
        {
            
        }

        public static bool ContainsUser(long UserId)
        {

            //throw new NotImplementedException();
            return false;
        }
    }
}