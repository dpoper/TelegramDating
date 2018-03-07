using System;
using System.Linq;
using System.Data.Linq;

namespace TelegramDating
{
    public static class Database
    {
        public static string ConnectLink { get; } = Properties.Settings.Default.DatabaseConnection;

        private static DataContext DB = new DataContext(ConnectLink);
        private static Table<User> UsersTable = DB.GetTable<User>();

        private static int NewId { get => UsersTable.Count(); }

        /// <summary>
        /// Print users from table.
        /// </summary>
        public static void Show()
        {
            try
            {
                var str = string.Join("\n", UsersTable.Select(u => u.Name).ToList());
                Console.WriteLine(str);
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

        /// <summary>
        /// Checks whether table contains user.
        /// </summary>
        /// <param name="Username">e.g durov (without '@' symbol)</param>
        /// <returns></returns>
        public static bool ContainsUser(string Username) => UsersTable
                                                            .Where(user => user.Username == Username)
                                                            .Count() > 0;
        


        
    }
}