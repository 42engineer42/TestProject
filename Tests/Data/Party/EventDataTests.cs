using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullam.Data;
using Nullam.Data.Party;

namespace Nullam.Tests.Data.Party {
    [TestClass] public class EventDataTests : SealedClassTests<EventData, BaseData> {
        [TestMethod] public void NameTest() => IsProperty<string?>();
        [TestMethod] public void DateTest() => IsProperty<DateTime?>();
        [TestMethod] public void PlaceTest() => IsProperty<string?>();
    }
}
