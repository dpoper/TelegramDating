using System;

namespace TelegramDating
{
    public static class Database
    {
        /// <summary>
        /// Add user to database.
        /// </summary>
        /// <param name="User"></param>
        public static void AddUser(User User)
        {
            throw new NotImplementedException();
        }

        // Пока не придумал, по какому па
        public static bool ContainsUser(User User) { throw new NotImplementedException(); }
        public static bool ContainsUser(long UserId) {

            //throw new NotImplementedException();
            return false;
        }
    }
}