using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramDating
{
    public static class State
    {
        public enum Create
        {
            Name, // Start of the bot
            Age,
            Sex,
            // ...
        }

        public enum Search
        {
            // ...
        }

    }
}
