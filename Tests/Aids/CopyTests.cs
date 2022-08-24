using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullam.Aids;
using Nullam.Data.Party;

namespace Nullam.Tests.Aids {
    [TestClass] public class CopyTests : TypeTests{
        [TestMethod] public void PropertiesTest() {
            var initA = new PersonData() { 
                FirstName = GetRandom.String(),
                LastName = GetRandom.String()
            };
            var initB = new PersonData();
            Copy.Properties(initA, initB);
            AreEqual(initA.FirstName, initB.FirstName);
            AreEqual(initA.LastName, initB.LastName);
        }
    }
}
