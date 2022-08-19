using System.Reflection;

namespace Nullam.Aids {
    public static class Types {
        private static readonly BindingFlags AllDeclaredOnly =
            BindingFlags.DeclaredOnly
            | BindingFlags.Public
            | BindingFlags.Instance
            | BindingFlags.Static;
        public static bool BelongsTo(this Type? type, string? namespaceName)
            => (type is not null) && type.IsRealType() && type.NameStarts(namespaceName);
        public static bool NameIs(this Type? type, string? name)
            => Safe.Run(() => name is not null && (type?.FullName?.Equals(name) ?? false));
        public static bool NameEnds(this Type? type, string? name)
            => Safe.Run(() => name is not null && (type?.FullName?.EndsWith(name) ?? false));
        public static bool NameStarts(this Type? type, string? name)
            => Safe.Run(() => name is not null && (type?.FullName?.StartsWith(name) ?? false));
        public static bool IsRealType(this Type? type)
            => Safe.Run(() => type?.FullName?.IsTypeFullName() ?? false);
        public static string? GetName(this Type? type) => type?.Name ?? string.Empty;
        public static List<string>? GetDeclaredMembers(this Type? type)
            => type?.GetMembers(AllDeclaredOnly)?.ToList()?.Select(x => x.Name)?.ToList() ?? new();
        public static bool IsInherited(this Type? type, Type subclass)
            => Safe.Run(() => type?.IsSubclassOf(subclass) ?? false, false);
        public static bool HasAttribute<TAttribute>(this Type? type) where TAttribute : Attribute
            => Safe.Run(() => type?.GetCustomAttributes<TAttribute>()?.FirstOrDefault() is not null, false);
        public static MethodInfo? GetMethod(this Type? type, string methodName) => Safe.Run(() => type?.GetMethod(methodName));
    }
}
