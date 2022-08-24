using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullam.Aids;
using Nullam.Data;
using Nullam.Data.Party;

namespace Nullam.Tests.Aids {
    [TestClass] public class TypesTests : TypeTests {
        private Type type = typeof(object);
        private string? nameSpace;
        private string? fullName;
        private string? name;
        private string? randomStr;
        [TestInitialize] public void Init() {
            type = typeof(PersonData);
            nameSpace = type.Namespace;
            fullName = type.FullName;
            name = type.Name;
            randomStr = GetRandom.String();
        }
        [TestMethod] public void BelongsToTest() {
            IsTrue(type.BelongsTo(nameSpace));
            IsFalse(type.BelongsTo(randomStr));
        }
        [TestMethod] public void NameEndsTest() {
            IsTrue(type.NameEnds(name));
            IsFalse(type.NameEnds(randomStr));
        }
        [TestMethod] public void NameStartsTest() {
            IsTrue(type.NameStarts(nameSpace));
            IsFalse(type.NameStarts(randomStr));
        }
        [TestMethod] public void IsRealTypeTest() {
            IsTrue(type.IsRealType());
            IsTrue(typeof(BaseData).IsRealType());
            var a = GetAssembly.OfType(this);
            var allTypes = (a?.GetTypes() ?? Array.Empty<Type>()).ToList();
            var realTypes = allTypes?.FindAll(t => t.IsRealType());
            IsNotNull(realTypes);
            IsTrue(realTypes.Count < (allTypes?.Count ?? 0));
            IsTrue(realTypes.Count > 0);
        }
        [TestMethod] public void GetNameTest() {
            AreEqual(name, type.GetName());
            AreNotEqual(randomStr, type.GetName());
        }
        [TestMethod] public void GetDeclaredMembersTest() {
            var l = typeof(BaseData)?.GetDeclaredMembers();
            AreEqual(11, l?.Count);
        }
        [TestMethod] public void IsInheritedTest() {
            Type? nullType = null;
            IsTrue(type.IsInherited(typeof(object)));
            IsTrue(type.IsInherited(typeof(BaseData)));
            IsFalse(type.IsInherited(nullType));
            IsFalse(type.IsInherited(typeof(PersonData)));
        }
        [TestMethod] public void HasAttributeTest() {
            IsFalse(type.HasAttribute<TestClassAttribute>());
            IsTrue(GetType().HasAttribute<TestClassAttribute>());
            IsFalse(type.HasAttribute<TestMethodAttribute>());
            IsFalse(GetType().HasAttribute<TestMethodAttribute>());
        }
        [TestMethod] public void MethodTest() {
            var n = nameof(MethodTest);
            var m = GetType().GetMethod(n);
            AreEqual(n, m?.Name);
        }
    }
}