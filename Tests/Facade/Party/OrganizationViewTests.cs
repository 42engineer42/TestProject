using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullam.Data;
using Nullam.Data.Party;
using Nullam.Domain.Party;
using Nullam.Facade;
using Nullam.Facade.Party;

namespace Nullam.Tests.Facade.Party {
    [TestClass] public class OrganizationViewTests : SealedClassTests<OrganizationView, BaseView> {
        [TestMethod] public void NameTest() => IsRequired<string?>("Juriidiline nimi");
        [TestMethod] public void ParticipantsAmountTest() => IsDisplayNamed<int?>("Osavõtjate arv");
        [TestMethod] public void OrganizationCodeTest() => IsDisplayNamed<string?>("Registrikood");
        [TestMethod] public void PayingTypeTest() => IsDisplayNamed<PayingType?>("Maksmisviis");
        [TestMethod] public void EventIdTest() => IsDisplayNamed<string?>("Üritus");
    }

    [TestClass] public class OrganizationViewFactoryTests
        : ViewFactoryTests<OrganizationViewFactory, OrganizationView, Organization, OrganizationData> {
        protected override Organization ToObject(OrganizationData d) => new(d);
        [TestMethod] public override void CreateTest() { }
    }
}