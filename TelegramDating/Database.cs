using System;
using System.Linq;
using System.Data.Linq;

namespace TelegramDating
{
    public static class Database
    {
        #region Database Variables

        public static string ConnectLink { get; } = @"Data Source=(LocalDB)\MSSQLLocalDB;" +
                                                    @"AttachDbFilename=|DataDirectory|\Database.mdf;" +
                                                    @"Integrated Security=True";

        private static DataContext DB = new DataContext(ConnectLink);
        public static Table<User> UsersTable = DB.GetTable<User>();

        #endregion

        /// <summary>
        /// Print users from table.
        /// </summary>
        public static void Show()
        {
            try
            {
                var str = string.Join("\n", UsersTable.Select(u => u.Username).ToList());
                Console.WriteLine(str);
            }
            catch (Exception ex) { Console.WriteLine("Show: " + ex.Message); }

        }

        /// <summary>
        /// Shortcut.
        /// </summary>
        public static void Submit() => UsersTable.Context.SubmitChanges();

        /// <summary>
        /// Add user into database.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>True if OK.</returns>
        public static bool Add(User user)
        {
            try
            {
                UsersTable.InsertOnSubmit(user);
                Database.Submit();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Пизда: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Checks whether table contains user.
        /// </summary>
        /// <param name="Username">e.g durov (without '@' symbol)</param>
        /// <returns></returns>
        public static bool Contains(string Username) => UsersTable.Count(user => user.Username == Username)> 0;

        /// <summary>
        /// Checks whether table contains user.
        /// </summary>
        /// <param name="Id">User's Telegram Id.</param>
        /// <returns></returns>
        public static bool Contains(long Id) => UsersTable.Count(user => user.Id == Id)> 0;

        public static User Get(long Id) => UsersTable.SingleOrDefault(user => user.Id == Id);
    }
}