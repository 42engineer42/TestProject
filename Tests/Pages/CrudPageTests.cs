using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullam.Domain;
using Nullam.Domain.Party;
using Nullam.Facade.Party;
using Nullam.Infra;
using Nullam.Infra.Party;
using Nullam.Pages;

namespace Nullam.Tests.Pages {
    [TestClass] public class CrudPageTests : AbstractClassTests<CrudPage<PersonView, Person, PersonsRepo>, BasePage<PersonView, Person, PersonsRepo>> {
        private class TestClass : CrudPage<PersonView, Person, PersonsRepo> {
            public TestClass(PersonsRepo? s) : base(s) { }
            protected internal override IActionResult RedirectToDelete(string id) => throw new NotImplementedException();
            protected internal override IActionResult RedirectToEdit(PersonView v) => throw new NotImplementedException();
            protected internal override IActionResult RedirectToIndex() => throw new NotImplementedException();
            protected internal override void SetAttributes(int idx, string? filter, string? order)
                => throw new NotImplementedException();
            protected internal override Person ToObject(PersonView? item) => throw new NotImplementedException();
            protected internal override PersonView ToView(Person? entity) => throw new NotImplementedException();
        }
        protected override CrudPage<PersonView, Person, PersonsRepo> CreateObj() {
            var db = GetRepo.Instance<NullamDb>();
            var repo = new PersonsRepo(db);
            return new TestClass(repo);
        }
        [TestMethod] public void GetCreateTest() => IsInconclusive();
        [TestMethod] public void GetItemPageTest() => IsInconclusive();
        [TestMethod] public void ItemPageTest() => IsInconclusive();
        [TestMethod] public void GetDetailsAsyncTest() => IsInconclusive();
        [TestMethod] public void GetDeleteAsyncTest() => IsInconclusive();
        [TestMethod] public void GetEditAsyncTest() => IsInconclusive();
        [TestMethod] public void PostCreateAsyncTest() => IsInconclusive();
        [TestMethod] public void PostDeleteAsyncTest() => IsInconclusive();
        [TestMethod] public void PostEditAsyncTest() => IsInconclusive();
        [TestMethod] public void GetIndexAsyncTest() => IsInconclusive();
        [TestMethod] public void GetItemTest() => IsInconclusive();
    }
}
