using NUnit.Framework;
using TelegramDating.Extensions;

namespace TelegramDating.Tests
{
    [TestFixture]
    public class ObjectExt_Test
    {
        const string actualDescription = "myDesc";

        [System.ComponentModel.Description(actualDescription)]
        private class MyClassWithDescriptionAttr { }

        [Test]
        public void TryGetDescriptionAttribute()
        {
            var desc = typeof(MyClassWithDescriptionAttr).GetDescription();

            Assert.AreEqual(actualDescription, desc);
        }

    }
}
