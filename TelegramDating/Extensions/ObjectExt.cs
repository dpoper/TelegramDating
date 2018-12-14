using System;
using System.ComponentModel;

namespace TelegramDating.Extensions
{
    internal static class ObjectExt
    {
        public static string GetDescription(this object obj)
        {
            var descriptionAttr = Attribute.GetCustomAttribute(obj.GetType(), typeof(DescriptionAttribute)) as DescriptionAttribute;

            return descriptionAttr.Description ?? "";
        }
    }
}
