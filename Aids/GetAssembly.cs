using System.Reflection;

namespace Nullam.Aids {
    public static class GetAssembly { 
        public static Assembly? GetAssemblyByName(string? name) => Safe.Run(() => Assembly.Load(name?? string.Empty));
        public static Assembly? OfType(object obj) => Safe.Run(() => obj.GetType().Assembly);
        public static List<Type>? GetTypes(Assembly? a) => Safe.Run(() => a?.GetTypes()?.ToList(), new ());
        public static Type? GetType(this Assembly? a, string? name) => Safe.Run(() => a?.DefinedTypes?.FirstOrDefault(x => x.Name == name));
    }
}
