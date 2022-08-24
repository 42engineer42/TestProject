using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullam.Facade;

namespace Nullam.Tests.Facade {
    [TestClass] public class BaseViewTests : AbstractClassTests<BaseView, object> {
        private class TestClass : BaseView { }
        protected override BaseView CreateObj() => new TestClass();
        [TestMethod] public void IdTest() => IsProperty<string>();
        [TestMethod] public void AdditionalInfoTest() => IsProperty<string>();
        [TestMethod] public void TokenTest() => IsProperty<byte[]>();
    }
}