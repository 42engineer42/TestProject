using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullam.Domain;
using Nullam.Domain.Party;
using Nullam.Facade.Party;
using Nullam.Infra;
using Nullam.Infra.Party;
using Nullam.Pages;

namespace Nullam.Tests.Pages {
    [TestClass] public class FilteredPageTests : AbstractClassTests<FilteredPage<PersonView, Person, PersonsRepo>, CrudPage<PersonView, Person, PersonsRepo>> {
        private class TestClass : FilteredPage<PersonView, Person, PersonsRepo> {
            public TestClass (PersonsRepo? s) : base(s) { }
            protected internal override IActionResult RedirectToDelete(string id) => throw new NotImplementedException();
            protected internal override IActionResult RedirectToEdit(PersonView v) => throw new NotImplementedException();
            protected internal override IActionResult RedirectToIndex() => throw new NotImplementedException();
            protected internal override void SetAttributes(int idx, string? filter, string? order)
                => throw new NotImplementedException();
            protected internal override Person ToObject(PersonView? item) => throw new NotImplementedException();
            protected internal override PersonView ToView(Person? entity) => throw new NotImplementedException();
        }
        protected override FilteredPage<PersonView, Person, PersonsRepo> CreateObj() {
            var db = GetRepo.Instance<NullamDb>();
            var repo = new PersonsRepo(db);
            return new TestClass(repo);
        }
        [TestMethod] public void CurrentFilterTest() => IsProperty<string?>();
        
    }
}
