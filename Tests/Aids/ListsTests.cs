using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullam.Aids;
using Nullam.Data.Party;

namespace Nullam.Tests.Aids {
    [TestClass] public class ListsTests : TypeTests {
        private List<int> list = new();
        [TestInitialize] public void Init() => list = new List<int>() { 1, 2, 3, 4, 5, 6 };
        [TestMethod] public void GetFirstTest() => AreEqual(1, Lists.GetFirst(list));
        [TestMethod] public void RemoveTest() {
            int? cnt = Lists.Remove(list, x => x < 4);
            AreEqual(3, cnt);
            AreEqual(4, Lists.GetFirst(list));
        }
        [TestMethod] public void IsEmptyTest() {
            List<PersonData>? countries = null;
            IsFalse(Lists.IsEmpty(list));
            IsTrue(Lists.IsEmpty(countries));
            IsTrue(Lists.IsEmpty(new List<string>()));
        }
        [TestMethod] public void ContainsItemTest() {
            IsTrue(Lists.ContainsItem(list, x => x < 2));
            IsFalse(Lists.ContainsItem(list, x => x > 6));
        }
    }
}