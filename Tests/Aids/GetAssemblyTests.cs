using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullam.Aids;
using Nullam.Data.Party;

namespace Nullam.Tests.Aids {
    [TestClass] public class GetAssemblyTests : TypeTests {
        private string? assemblyName;
        private Assembly? assembly;
        private string[] typenames = Array.Empty<string>();
        [TestInitialize] public void Init() {
            assemblyName = $"{nameof(Nullam)}.{nameof(Nullam.Aids)}";
            assembly = GetAssembly.GetAssemblyByName(assemblyName);
            typenames = new string[] { nameof(Chars), nameof(Enums), nameof(Lists)
                , nameof(Strings), nameof(Safe), nameof(Types) };
        }
        [TestCleanup] public void Clean() {
            IsNotNull(assembly);
            AreEqual(assemblyName, assembly.GetName().Name);
        }
        [TestMethod] public void ByNameTest() { }
        [TestMethod] public void OfTypeTest() {
            assemblyName = $"{nameof(Nullam)}.{nameof(Nullam.Data)}";
            var obj = new EventData();
            assembly = GetAssembly.OfType(obj);
        }
        [TestMethod] public void TypesTest() {
            var l = GetAssembly.GetTypes(assembly);
            IsTrue(typenames.Length <= (l?.Count ?? -1));
            foreach (var n in typenames) AreEqual(l?.FirstOrDefault(x => x.Name == n)?.Name, n);
            IsNull(l?.FirstOrDefault(x => x.Name == GetRandom.String()));
        }
    }
}