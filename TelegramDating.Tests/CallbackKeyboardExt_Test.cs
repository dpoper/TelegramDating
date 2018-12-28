using System.Linq;
using NUnit.Framework;
using TelegramDating.Extensions;
using TelegramDating.Model;

namespace TelegramDating.Tests
{
    [TestFixture]
    public class CallbackKeyboardExt_Test
    {
        [OneTimeSetUp]
        public void Container_UserContext_Install()
        {
            Container.Current.Install(new Database.Installer());
        }

        [OneTimeTearDown]
        public void Container_UserContext_Dispose()
        {
            Container.Current.Resolve<Database.UserContext>().Dispose();
        }

        [Test]
        public void CreateLikeDislikeKeyboard_Request()
        {
            var user = new User(1337, "durov");

            var buttons = CallbackKeyboardExt.CreateLikeDislikeKeyboard(user)
                                            .InlineKeyboard
                                            .First().ToArray();

            Assert.AreEqual(CallbackKeyboardExt.EmojiConsts.Heart, buttons[0].Text);
            Assert.AreEqual(CallbackKeyboardExt.EmojiConsts.BrokenHeart, buttons[1].Text);

            Assert.AreEqual("req 1337 true", buttons[0].CallbackData);
            Assert.AreEqual("req 1337 false", buttons[1].CallbackData);
        }

        [Test]
        public void CreateLikeDislikeKeyboard_Response()
        {
            var user = new User(1337, "durov");

            var buttons = CallbackKeyboardExt.CreateLikeDislikeKeyboard(user, isForResponse: true)
                                            .InlineKeyboard
                                            .First().ToArray();

            Assert.AreEqual(CallbackKeyboardExt.EmojiConsts.Heart, buttons[0].Text);
            Assert.AreEqual(CallbackKeyboardExt.EmojiConsts.BrokenHeart, buttons[1].Text);

            Assert.AreEqual("resp 1337 true", buttons[0].CallbackData);
            Assert.AreEqual("resp 1337 false", buttons[1].CallbackData);
        }

        [Test]
        public void ExtractLike_From_RequestLike()
        {
            var buttons = CallbackKeyboardExt.CreateLikeDislikeKeyboard(new User(7331, "misha"))
                .InlineKeyboard.First().ToArray();

            Like likeExtracted = CallbackKeyboardExt.ExtractLike(buttons[0].CallbackData);
            Assert.AreEqual(true, likeExtracted.Liked, "Like  bool .Liked");

            Like dislikeExtracted = CallbackKeyboardExt.ExtractLike(buttons[1].CallbackData);
            Assert.AreEqual(false, dislikeExtracted.Liked, "Dislike bool .Liked");
        }

        [Test]
        public void ExtractLike_From_ResponseLike()
        {
            var user = new User(7331, "misha");
            user.GotLikes.Add(new Like(user, true) { User = new User(1337, "durov")});

            var buttons = CallbackKeyboardExt.CreateLikeDislikeKeyboard(user.GotLikes.First().User, true)
                .InlineKeyboard.First().ToArray();

            Like likeExtracted = CallbackKeyboardExt.ExtractLike(buttons[0].CallbackData, user.GotLikes.First());
            Assert.AreEqual(true, likeExtracted.Response, "Like response");

            Like dislikeExtracted = CallbackKeyboardExt.ExtractLike(buttons[1].CallbackData, user.GotLikes.First());
            Assert.AreEqual(false, dislikeExtracted.Response, "Dislike response");
        }
    }
}
