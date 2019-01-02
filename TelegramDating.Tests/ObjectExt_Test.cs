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
        public void GetDescription_FromClass_With_DescriptionAttribute()
        {
            var descWhereHasAttribute = typeof(MyClassWithDescriptionAttr).GetDescription();
            
            Assert.AreEqual(expectedDescription, descWhereHasAttribute);
        }

        [Test]
        public void GetDescription_FromClass_Without_DescriptionAttribute_ReturnNull()
        {
            var descWhereHasNoAttribute = typeof(MyClassWithoutDescriptionAttr).GetDescription();
            
            Assert.AreEqual(null, descWhereHasNoAttribute);
        }

    }
}
