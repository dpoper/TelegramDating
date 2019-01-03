using System;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using TelegramDating.Bot.Commands.AskActions;
using TelegramDating.Bot.Commands.Slash;
using TelegramDating.Database;
using TelegramDating.Enums;
using TelegramDating.Extensions;
using TelegramDating.Model;

namespace TelegramDating.Bot
{
	public partial class BotWorker
	{
		public UserContext UserContext { get; set; }
		public IReadOnlyList<SlashCommand> AvailableSlashCommandList { get; private set; } = new SlashCommand[] {
																										new HelpCommand(),
																										new StartCommand(),
																										new ResetCommand(),
																										new MyProfileCommand(),
																								  };

		public IList<AskAction> ProfileCreatingAskActions { get; private set; } =
			new List<AskAction>
			{
				new AskName(),
				new AskSex(),
				new AskAge(),
				new AskCountry(),
				new AskCity(),
				new AskAbout(),
				new AskSearchSex(),
				new AskPicture(),
			};

		private static TelegramBotClient _client { get; set; }

		public TelegramBotClient Instance => _client;

		public BotWorker(UserContext UserContext)
		{
			this.UserContext = UserContext;
			BotWorker.Create();
		}

		/// <summary>
		/// Create instance of bot.
		/// </summary>
		/// <param name="token">Telegram bot token. Use @BotFather to get your bot's token.</param>
		/// <returns></returns>
		public static TelegramBotClient Create(string token = Program.Token)
		{
			if (_client != null) return _client;

			_client = new TelegramBotClient(token);

			return _client;
		}

		public User StartNewUser(long userId, string username)
		{
			User currentUser = new User(userId, username);
			currentUser.ProfileCreatingState = (ProfileCreatingEnum?) Container.Current.Resolve<BotWorker>().FindAskAction(0).Id;

			this.FindSlashCommand("/start").Execute(currentUser);
			return currentUser;
		}

		protected async void SendLikeDislikeKeyboard(User forUser, User profileUser, bool isForResponse = false)
		{
			await this.Instance.SendPhotoAsync(
				chatId: forUser.UserId,
				photo: profileUser.PictureId,
				caption: MessageFormatter.FormatProfileMessage(profileUser, isForResponse),
				parseMode: ParseMode.Html,
				replyMarkup: CallbackKeyboardExt.CreateLikeDislikeKeyboard(profileUser, isForResponse));
		}

		public async void NotifyMatchedBoth(User firstUser, User secondUser)
		{
			await this.Instance.SendTextMessageAsync(firstUser.UserId, MessageFormatter.GetYouHaveBeenMatchedText(secondUser), ParseMode.Html);
			await this.Instance.SendTextMessageAsync(secondUser.UserId, MessageFormatter.GetYouHaveBeenMatchedText(firstUser), ParseMode.Html);
		}

		public async void TrySendNextProfile(User forUser)
		{
			var profileUser = this.FindSomeoneForUser(forUser);

			if (profileUser == null)
			{
				await this.Instance.SendTextMessageAsync(forUser.UserId,
					"Мы никого для тебя не нашли...\n" +
					"Просто пришли мне попозже какое-нибудь текстовое сообщение, чтобы проверить, появились ли новые пользователи!");
				return;
			}

			this.SendLikeDislikeKeyboard(forUser, profileUser);
		}

		public User FindSomeoneForUser(User currentUser)
		{
			IEnumerable<long> likedIds = this.UserContext.LoadLikes(currentUser).Select(like => like.CheckedUser.Id);
			IEnumerable<long> gotLikesFromIds = this.UserContext.LoadGotLikes(currentUser).Select(like => like.User.Id);
			var usersForSearch = this.UserContext.Users
				.Where(u => u.UserId != currentUser.UserId)   // Не я
				.Where(u => u.ProfileCreatingState == null)   // Не создает профиль
				.Where(u => u.DeletedAt == null)              // Не мёртв
				.Where(u => !likedIds.Contains(u.Id))         // Я не видел его анкету
				.Where(u => !gotLikesFromIds.Contains(u.Id)); // Он не видел мою анкету

			return usersForSearch.FirstOrDefault();
		}

		// Returns bool isSent.
		public bool TrySendNextProfileForResponse(User forUser)
		{
			var profileUser = this.FindSomeoneFromGotLikes(forUser);

			if (profileUser == null)
				return false;

			this.SendLikeDislikeKeyboard(forUser, profileUser, true);
			return true;
		}

		public User FindSomeoneFromGotLikes(User currentUser)
		{
			//.Where(foundUser => currentUser.SearchSex == foundUser.Sex)
			return this.UserContext.LoadGotLikes(currentUser)
								   .Where(x => x.User.ProfileCreatingState == null)
								   .Where(x => x.User.DeletedAt == null)
								   .FirstOrDefault(x => x.Response == null)?.User;
		}

		public async void RemoveKeyboard(Telegram.Bot.Types.CallbackQuery cquery)
		{
			await this.Instance.EditMessageReplyMarkupAsync(cquery.Message.Chat.Id, cquery.Message.MessageId, replyMarkup: null);
		}

		public AskAction FindAskAction(ProfileCreatingEnum pce)
		{
			return this.ProfileCreatingAskActions.FirstOrDefault(aa => aa.Id == (int) pce);
		}

		public int GetAskActionIndex(ProfileCreatingEnum pce)
		{
			return this.ProfileCreatingAskActions.IndexOf(pce);
		}

		public AskAction GetNextAskAction(ProfileCreatingEnum pce)
		{
			int nextIndex = this.GetAskActionIndex(pce) + 1;

			if (nextIndex < this.ProfileCreatingAskActions.Count)
				return this.ProfileCreatingAskActions.ElementAt(nextIndex);
			else
				return null;
		}

		public AskAction FindAskAction(int index)
		{
			return this.ProfileCreatingAskActions.ElementAt(index);
		}

		public SlashCommand FindSlashCommand(string messageText)
		{
			return this.AvailableSlashCommandList.SingleOrDefault(cmd => messageText == cmd.SlashText);
		}

		public async void SendNoUsernameSetMessage(long userId)
		{
			await this.Instance.SendTextMessageAsync(userId,
					"Упс! У тебя не проставлен юзернейм. Сделай так, чтобы к тебе можно было обращаться с собачкой. \n" +
					$"Как ко мне: @{this.Instance.GetMeAsync().Result.Username}");
		}
	}
}
