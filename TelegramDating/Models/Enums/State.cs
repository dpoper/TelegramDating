namespace TelegramDating
{
    public static class State
    {
        public enum Create : int
        {
            Name = 1, // Start of the bot
            Age = 2,
            Sex = 3,
            Country = 4,
            City = 5,
            Picture = 6,
        }

        public enum Search : int
        {
            Show = 11,
        }
    }
}

