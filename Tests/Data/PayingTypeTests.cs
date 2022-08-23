using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullam.Aids;
using Nullam.Data;

namespace Nullam.Tests.Data {
    [TestClass] public abstract class PayingTypeTests : TypeTests {
        [TestMethod] public void TransferTest() => DoTest(PayingType.Transfer, 0, "Pangaülekanne");
        [TestMethod] public void CashTest() => DoTest(PayingType.Cash, 1, "Sularaha");
        private static void DoTest(PayingType type, int value, string description) {
            AreEqual(value, (int)type);
            AreEqual(description, type.GetDescription());
        }
    }
}

