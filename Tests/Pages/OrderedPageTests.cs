using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullam.Domain;
using Nullam.Domain.Party;
using Nullam.Facade.Party;
using Nullam.Infra;
using Nullam.Infra.Party;
using Nullam.Pages;

namespace Nullam.Tests.Pages {
    [TestClass] public class OrderedPageTests : AbstractClassTests<OrderedPage<PersonView, Person, PersonsRepo>, FilteredPage<PersonView, Person, PersonsRepo>> {
        private class TestClass : OrderedPage<PersonView, Person, PersonsRepo> {
            public TestClass(PersonsRepo? s) : base(s) { }
            protected internal override IActionResult RedirectToDelete(string id) => throw new NotImplementedException();
            protected internal override IActionResult RedirectToEdit(PersonView v) => throw new NotImplementedException();
            protected internal override IActionResult RedirectToIndex() => throw new NotImplementedException();
            protected internal override void SetAttributes(int idx, string? filter, string? order)
                => throw new NotImplementedException();
            protected internal override Person ToObject(PersonView? item) => throw new NotImplementedException();
            protected internal override PersonView ToView(Person? entity) => throw new NotImplementedException();
        }
        protected override OrderedPage<PersonView, Person, PersonsRepo> CreateObj() {
            var db = GetRepo.Instance<NullamDb>();
            var repo = new PersonsRepo(db);
            return new TestClass(repo);
        }
        [TestMethod] public void CurrentOrderTest() => IsInconclusive();
        [TestMethod] public void fromCurrentOrderTest() => IsInconclusive();
        [TestMethod] public void toCurrentOrderTest() => IsInconclusive();
        [TestMethod] public void SortOrderTest() => IsInconclusive();

        [DataRow("FirstName", "Eesnimi")]
        [DataRow("LastName", "Perenimi")]
        [TestMethod] public void GetDisplayNameTest(string name, string displayName) {
            var pi = typeof(PersonView).GetProperty(name);
            var actual = OrderedPage<PersonView, Person, PersonsRepo>.GetDisplayName(pi);
            AreEqual(actual, displayName);
        }

        [DataRow("FirstName", "Eesnimi", true)]
        [DataRow("FirstName", "Koht", false)]
        [DataRow("LastName", "Perenimi", true)]
        [TestMethod] public void IsThisDisplayNameTest(string name, string displayName, bool expected) {
            var pi = typeof(PersonView).GetProperty(name);
            var actual = OrderedPage <PersonView, Person, PersonsRepo>.IsThisDisplayName(pi, displayName);
            AreEqual(actual, expected);
        }
    }
}
