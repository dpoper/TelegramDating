using System;
using System.ComponentModel;

namespace TelegramDating.Extensions
{
    public static class ObjectExt
    {
        public static string GetDescription(this Type type)
        {
            var descriptionAttr = Attribute.GetCustomAttribute(type, typeof(DescriptionAttribute)) as DescriptionAttribute;

            return descriptionAttr.Description ?? "";
        }
    }
}
