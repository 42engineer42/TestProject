using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullam.Aids;
using Nullam.Data.Party;

namespace Nullam.Tests.Aids {
    [TestClass] public class GetNamespaceTests : TypeTests {
        [TestMethod] public void OfTypeTest() {
            var obj = new PersonData();
            var name = obj.GetType().Namespace;
            var namespaceName = GetNamespace.OfType(obj);
            AreEqual(name, namespaceName);
        }
        [TestMethod] public void OfTypeNullTest() {
            PersonData? obj = null;
            var n = GetNamespace.OfType(obj);
            AreEqual(string.Empty, n);
        }
    }
}
