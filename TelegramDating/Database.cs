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

        private static DataContext DB;
        // private static Table<User> UsersTable = DB.GetTable<User>();

        public static void Test()
        {
            try
            {

            }
            catch (Exception ex)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("ПИЗДОС");
                Console.ResetColor();
            }


        }

        public static void AddTest()
        {


            var user = new User(4, "Dik", 11, 0, 124124);

            try
            {
                DB = new DataContext(ConnectLink);
                Table<User> UsersTable = DB.GetTable<User>();
                UsersTable.InsertOnSubmit(user);
                UsersTable.Context.SubmitChanges();
                Console.WriteLine(UsersTable.First().Name);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        /// <summary>
        /// Add user to database.
        /// </summary>
        /// <param name="User"></param>
        public static void AddUser(User user)
        {



        }

        // Пока не придумал, по какому па
        public static bool ContainsUser(User User)
        {
            throw new NotImplementedException();
        }

        public static bool ContainsUser(long UserId)
        {

            //throw new NotImplementedException();
            return false;
        }
    }
}