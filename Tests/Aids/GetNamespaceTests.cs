using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullam.Aids;
using Nullam.Data.Party;

namespace Nullam.Tests.Aids {
    [TestClass] public class GetNamespaceTests : TypeTests {
        [TestMethod] public void OfTypeTest() {
            PersonData? obj = new PersonData();
            string? name = obj.GetType().Namespace;
            string? namespaceName = GetNamespace.OfType(obj);
            AreEqual(name, namespaceName);
        }
        [TestMethod] public void OfTypeNullTest() {
            PersonData? obj = null;
            string? n = GetNamespace.OfType(obj);
            AreEqual(string.Empty, n);
        }
    }
}
