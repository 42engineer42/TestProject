using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullam.Aids;
using Nullam.Data.Party;
using Nullam.Domain.Party;
using Nullam.Facade;
using Nullam.Facade.Party;

namespace Nullam.Tests.Facade {
    [TestClass] public class BaseViewFactoryTests : AbstractClassTests<BaseViewFactory<PersonView, Person, PersonData>, object> {
        private class TestClass : BaseViewFactory<PersonView, Person, PersonData> {
            protected override Person ToEntity(PersonData d) => new(d);
        }
        protected override BaseViewFactory<PersonView, Person, PersonData> CreateObj() => new TestClass();
        [TestMethod] public void CreateTest() { }
        [TestMethod] public void CreateViewTest() {
            dynamic? expectedView = GetRandom.Value<PersonView>();
            dynamic? actualView = Obj.Create(expectedView);
            ArePropertiesEqual(expectedView, actualView.Data);
        }
        [TestMethod] public void CreateObjectTest() {
            dynamic? expectedObject = GetRandom.Value<PersonData>();
            PersonView actualObject = Obj.Create(new Person(expectedObject));
            ArePropertiesEqual(expectedObject, actualObject);
        }
    }
}