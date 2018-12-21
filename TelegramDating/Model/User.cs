using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TelegramDating.Bot;
using TelegramDating.Enums;

namespace TelegramDating.Model
{
    public sealed class User : Entity
    {
        /// <summary>
        /// Pass chat_id here.
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// Name that will be displayed to the person who found this user.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description that will be displayed to the person who found this user.
        /// </summary>
        public string About { get; set; }

        /// <summary>
        /// E.g durov (without '@' symbol at the beginning)
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Current user's current age.
        /// </summary>
        public int Age { get; set; }

        /// Current user's sex.
        /// 
        /// Male   = True
        /// Female = False
        public bool Sex { get; set; }

        /// <summary>
        /// Country name.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// City name.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Photo id from telegram server.
        /// Will be used as profile picture of current user.
        /// </summary>
        public string PictureId { get; set; }

        public DateTime LastVisitAt { get; set; } = DateTime.Now;
        
        public DateTime? DeletedAt { get; set; } = null;

        /// <summary>
        /// Current user's preferences
        ///     (Sex, City)
        /// </summary>
        public SearchOptions.Sex SearchSex { get; set; }

        /// <summary>
        /// Current user's profile state id.
        /// </summary>
        public ProfileCreatingEnum? ProfileCreatingState { get; set; } = (ProfileCreatingEnum?) Container.Current.Resolve<BotWorker>().FindAskAction(0).Id;

        public ICollection<Like> Likes { get; set; } = new Collection<Like>();

        public ICollection<Like> GotLikes { get; set; } = new Collection<Like>();

        /// <summary>
        /// Constructor for DB mapping.
        /// </summary>
        public User() { }

        /// <summary>
        /// Constructor for adding users into database.
        /// </summary>
        public User(long userId, string username)
        {
            this.UserId = userId;
            this.Username = username;
        }

        /// <summary>
        /// Sets info properties depending on ProfileCreatingState value.
        /// </summary>
        public void SetInfo(object infoObj)
        {
            if (!this.ProfileCreatingState.HasValue)
                return;

            string info = infoObj.ToString();

            switch (this.ProfileCreatingState)
            {
                case ProfileCreatingEnum.Sex:       this.Sex = (SearchOptions.Sex) int.Parse(info) == SearchOptions.Sex.Male;
                    break;
                case ProfileCreatingEnum.Name:      this.Name = info;
                    break;
                case ProfileCreatingEnum.About:     this.About = info;
                    break;
                case ProfileCreatingEnum.Age:       this.Age = int.Parse(info);
                    break;
                case ProfileCreatingEnum.Country:   this.Country = info;
                    break;
                case ProfileCreatingEnum.City:      this.City = info;
                    break;
                case ProfileCreatingEnum.Picture:   this.PictureId = (infoObj as Telegram.Bot.Types.PhotoSize).FileId;
                    break;
                case ProfileCreatingEnum.SearchSex: this.SearchSex = (SearchOptions.Sex) Enum.Parse(typeof(SearchOptions.Sex), info);
                break;

                default:
                    throw new ArgumentException($"Ой! Разработчик, кажется, забыл реализовать что-то.\n" +
                                                $"ProfileCreatingState: {this.ProfileCreatingState}");
            }
        }
    }
}
