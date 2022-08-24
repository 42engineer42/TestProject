using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullam.Aids;

namespace Nullam.Tests.Aids {
    [TestClass] public class StringsTests : TypeTests {
        private string? testStr;
        [TestInitialize] public void Init() => testStr = "a1b1c1.d1e1f1.g1h1i1";
        [TestMethod] public void RemoveTest() => AreEqual("abc.def.ghi", Strings.Remove(testStr, "1"));
        [TestMethod] public void IsTypeNameTest() {
            IsFalse(Strings.IsTypeName(testStr));
            var s = Strings.Remove(testStr, "1");
            IsFalse(Strings.IsTypeName(s));
            s = Strings.RemoveTail(s);
            IsFalse(Strings.IsTypeName(s));
            s = Strings.RemoveTail(s);
            IsTrue(Strings.IsTypeName(s));
        }
        [TestMethod] public void IsTypeFullNameTest() {
            IsTrue(Strings.IsTypeFullName(testStr));
            IsTrue(Strings.IsTypeFullName(Strings.Remove(testStr, "1")));
        }
        [TestMethod] public void RemoveTailTest() => AreEqual("a1b1c1.d1e1f1", Strings.RemoveTail(testStr));
        [TestMethod] public void RemoveHeadTest() => AreEqual("d1e1f1.g1h1i1", Strings.RemoveHead(testStr));
    }
}