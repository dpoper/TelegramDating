using NUnit.Framework;
using TelegramDating.Extensions;

namespace TelegramDating.Tests
{
    [TestFixture]
    public class ObjectExt_Test
    {
        const string expectedDescription = "myDesc";

        [System.ComponentModel.Description(expectedDescription)]
        private class MyClassWithDescriptionAttr { }
        
        private class MyClassWithoutDescriptionAttr { }

        [Test]
        public void TryGetDescriptionAttribute()
        {
            var descWhereHasAttribute = typeof(MyClassWithDescriptionAttr).GetDescription();
            var descWhereHasNoAttribute = typeof(MyClassWithoutDescriptionAttr).GetDescription();

            Assert.AreEqual(expectedDescription, descWhereHasAttribute);
            Assert.AreEqual(null, descWhereHasNoAttribute);
        }

    }
}
