using System.Reflection;

namespace Nullam.Aids {
    public static class Copy {
        public static void Properties(object? from, object? to) {
            Type? typeFrom = from?.GetType();
            Type? typeTo = to?.GetType();
            foreach (PropertyInfo piFrom in typeFrom?.GetProperties() ?? Array.Empty<PropertyInfo>()) {
                object? value = piFrom.GetValue(from, null);
                PropertyInfo? piTo = typeTo?.GetProperty(piFrom.Name);
                piTo?.SetValue(to, value, null);
            }
        }
    }
}
