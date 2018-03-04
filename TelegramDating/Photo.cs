namespace TelegramDating
{
    public class Photo
    {
        public int Id { get; }
        public string URL { get { return $"/{Id}.jpg"; } }
        
        public Photo(int Id)
        {
            this.Id = Id;
        }
    }
}