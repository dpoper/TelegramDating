using TelegramDating.Model.Enums;

namespace TelegramDating.Model.Commands.AskActions
{
    internal class AskPicture : AskAction
    {
        public override int Id => (int) ProfileCreatingEnum.Picture;

        public override async void Ask(User currentUser)
        {
            await Program.Bot.SendTextMessageAsync(currentUser.UserId,
                "Отлично! Последний штрих – твоё фото. Пришли мне картинку.");
        }
    }
}
