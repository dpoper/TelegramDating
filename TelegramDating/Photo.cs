namespace TelegramDating
{
    public class Photo
    {
        /// <summary>
        /// Photo id from Telegram servers.
        /// </summary>
        public int Id { get; }
        public string URL { get { return $"/{Id}.jpg"; } }
        
        public Photo(int Id)
        {
            this.Id = Id;
        }
    }
}