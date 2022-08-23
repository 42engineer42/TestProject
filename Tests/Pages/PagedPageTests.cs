using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullam.Domain;
using Nullam.Domain.Party;
using Nullam.Facade.Party;
using Nullam.Infra;
using Nullam.Infra.Party;
using Nullam.Pages;

namespace Nullam.Tests.Pages {
    [TestClass] public class PagedPageTests : AbstractClassTests<PagedPage<PersonView, Person, PersonsRepo>, OrderedPage<PersonView, Person, PersonsRepo>> {
        private class TestClass : PagedPage<PersonView, Person, PersonsRepo> {
            public TestClass(PersonsRepo? s) : base(s) { }
            protected internal override IActionResult RedirectToDelete(string id) => throw new NotImplementedException();
            protected internal override IActionResult RedirectToEdit(PersonView v) => throw new NotImplementedException();
            protected internal override IActionResult RedirectToIndex() => throw new NotImplementedException();
            protected internal override void SetAttributes(int idx, string? filter, string? order)
                => throw new NotImplementedException();
            protected internal override Person ToObject(PersonView? item) => throw new NotImplementedException();
            protected internal override PersonView ToView(Person? entity) => throw new NotImplementedException();
        }
        protected override PagedPage<PersonView, Person, PersonsRepo> CreateObj() {
            var db = GetRepo.Instance<NullamDb>();
            var repo = new PersonsRepo(db);
            return new TestClass(repo);
        }

        [TestMethod] public void PageIndexTest() => IsProperty<int>();
        [TestMethod] public void TotalPagesTest() => IsReadOnly<int>();
        [TestMethod] public void HasNextPageTest() => IsReadOnly<bool>();
        [TestMethod] public void HasPreviousPageTest() => IsReadOnly<bool>();
        [TestMethod] public void IndexColumnsTest() => IsReadOnly<string[]>();

        [TestMethod] public void SetAttributesTest() => IsInconclusive();
        [TestMethod] public void RedirectToIndexTest() => IsInconclusive();
        [TestMethod] public void RedirectToEditTest() => IsInconclusive();
        [TestMethod] public void RedirectToDeleteTest() => IsInconclusive();
        [TestMethod] public void GetValueTest() => IsInconclusive();
        [TestMethod] public void DisplayNameTest() => IsInconclusive();
    }
}
