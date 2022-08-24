using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullam.Aids;
using Nullam.Data;

namespace Nullam.Tests.Aids {
    [TestClass] public class EnumsTests : TypeTests {
        [TestMethod] public void DescriptionTest()
             => AreEqual("Sularaha", Enums.GetDescription(PayingType.Cash));
        [TestMethod] public void ToStringTest()
              => AreEqual("Transfer", PayingType.Transfer.ToString());
    }
}