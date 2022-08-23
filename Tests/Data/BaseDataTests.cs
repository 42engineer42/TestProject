using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullam.Data;

namespace Nullam.Tests.Data {
    [TestClass] public class BaseDataTests : AbstractClassTests<BaseData, object> {
        private class TestClass : BaseData { }
        protected override BaseData CreateObj() => new TestClass();
        [TestMethod] public void IdTest() => IsProperty<string>();
        [TestMethod] public void AdditionalInfoTest() => IsProperty<string>();
        [TestMethod] public void TokenTest() => IsProperty<byte[]>();
        [TestMethod] public void NewIdTest() {
            IsNotNull(BaseData.NewId);
            AreNotEqual(BaseData.NewId,BaseData.NewId);
            PropertyInfo? propertyInfo = typeof(BaseData).GetProperty(nameof(BaseData.NewId));
            IsFalse(propertyInfo?.CanWrite);
        }
    }
}
