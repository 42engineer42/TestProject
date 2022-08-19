using System.ComponentModel;
using System.Reflection;

namespace Nullam.Aids {
    public static class Enums {
        public static string GetDescription(this Enum input) {
            FieldInfo? info = input.GetType().GetField(input.ToString());
            DescriptionAttribute? attribute = info?.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute;
            return attribute?.Description ?? input.ToString();
        }
    }
}
