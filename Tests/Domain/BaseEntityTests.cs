using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullam.Aids;
using Nullam.Data.Party;
using Nullam.Domain;

namespace Nullam.Tests.Domain {
    [TestClass]
    public class BaseEntityTests : AbstractClassTests<BaseEntity<EventData>, BaseEntity> {
        private EventData? d;
        private class TestClass : BaseEntity<EventData> {
            public TestClass() : this(new EventData()) { }
            public TestClass(EventData d) : base(d) { }
        }
        protected override BaseEntity<EventData> CreateObj() {
            d = GetRandom.Value<EventData>();
            return new TestClass(d);
        }
        [TestMethod] public void IdTest() => IsReadOnly(Obj.Data.Id);
        [TestMethod] public void AdditionalInfoTest() => IsReadOnly(Obj.Data.AdditionalInfo);
        [TestMethod] public void DataTest() => IsReadOnly(d);
        [TestMethod] public void DefaultStrTest() => AreEqual("Undefined", BaseEntity.DefaultStr);
        [TestMethod] public void DefaultIntTest() => AreEqual(0, BaseEntity.DefaultInt);
        [TestMethod] public void DefaultDateTest() => AreEqual(DateTime.MinValue, BaseEntity.DefaultDate);
        [TestMethod] public void TokenTest() => IsReadOnly(Obj.Data.Token);
    }
}