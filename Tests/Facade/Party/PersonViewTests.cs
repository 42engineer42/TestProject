using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullam.Data;
using Nullam.Data.Party;
using Nullam.Domain.Party;
using Nullam.Facade;
using Nullam.Facade.Party;

namespace Nullam.Tests.Facade.Party {
    [TestClass] public class PersonViewTests : SealedClassTests<PersonView, BaseView> {
        [TestMethod] public void FirstNameTest() => IsRequired<string?>("Eesnimi");
        [TestMethod] public void LastNameTest() => IsDisplayNamed<string?>("Perenimi");
        [TestMethod] public void PersonalCodeTest() => IsDisplayNamed<string?>("Isikukood");
        [TestMethod] public void PayingTypeTest() => IsDisplayNamed<PayingType?>("Maksmisviis");
        [TestMethod] public void EventIdTest() => IsDisplayNamed<string?>("Üritus");
    }

    [TestClass] public class PersonViewFactoryTests
        : ViewFactoryTests<PersonViewFactory, PersonView, Person, PersonData> {
        protected override Person ToObject(PersonData d) => new(d);
        [TestMethod] public override void CreateTest() { }
    }
}