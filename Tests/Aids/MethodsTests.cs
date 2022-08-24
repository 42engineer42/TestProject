using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullam.Aids;

namespace Nullam.Tests.Aids {
    [TestClass] public class MethodsTests : TypeTests {
        [TestMethod] public void HasAttributeTest() {
            MethodInfo? m = GetType().GetMethod(nameof(HasAttributeTest));
            IsTrue(Methods.HasAttribute<TestMethodAttribute>(m));
            IsFalse(Methods.HasAttribute<TestInitializeAttribute>(m));
        }
        [TestMethod] public void GetAttributeTest() {
            MethodInfo? m = GetType().GetMethod(nameof(GetAttributeTest));
            IsNotNull(Methods.GetAttribute<TestMethodAttribute>(m));
            IsNull(Methods.GetAttribute<TestInitializeAttribute>(m));
        }
    }
}
