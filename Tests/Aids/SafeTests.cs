using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullam.Aids;

namespace Nullam.Tests.Aids {
    [TestClass]
    public class SafeTests : TypeTests {
        private int expected;
        private int def;
        [TestInitialize] public void Init() {
            expected = GetRandom.Int32();
            def = GetRandom.Int32();
        }
        [TestMethod] public void RunFuncTest() {
            var actual = Safe.Run(() => expected, def);
            AreEqual(expected, actual);
        }
        [TestMethod] public void RunFuncExceptionTest() {
            var actual = Safe.Run(() => throw new Exception(), def);
            AreEqual(def, actual);
        }
        [TestMethod] public void RunTest() { }
    }
}