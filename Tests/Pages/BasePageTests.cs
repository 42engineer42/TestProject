using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullam.Domain;
using Nullam.Domain.Party;
using Nullam.Facade.Party;
using Nullam.Infra;
using Nullam.Infra.Party;
using Nullam.Pages;

namespace Nullam.Tests.Pages {
    [TestClass] public class BasePageTests : AbstractClassTests<BasePage<PersonView, Person, PersonsRepo>, PageModel> {
        private class TestClass : BasePage<PersonView, Person, PersonsRepo> {
            public TestClass(PersonsRepo? s) : base(s) { }
            protected internal override IActionResult GetCreate() => throw new System.NotImplementedException();
            protected internal override Task<IActionResult> GetDeleteAsync(string id) => throw new System.NotImplementedException();
            protected internal override Task<IActionResult> GetDetailsAsync(string id) => throw new System.NotImplementedException();
            protected internal override Task<IActionResult> GetEditAsync(string id) => throw new System.NotImplementedException();
            protected internal override Task<IActionResult> GetIndexAsync() => throw new System.NotImplementedException();
            protected internal override Task<IActionResult> PostCreateAsync() => throw new System.NotImplementedException();
            protected internal override Task<IActionResult> PostDeleteAsync(string id, string? token = null)
                => throw new System.NotImplementedException();
            protected internal override Task<IActionResult> PostEditAsync() => throw new System.NotImplementedException();
            protected internal override IActionResult RedirectToDelete(string id) => throw new System.NotImplementedException();
            protected internal override IActionResult RedirectToEdit(PersonView v) => throw new System.NotImplementedException();
            protected internal override IActionResult RedirectToIndex() => throw new System.NotImplementedException();
            protected internal override void SetAttributes(int idx, string? filter, string? order)
                => throw new System.NotImplementedException();
            protected internal override Person ToObject(PersonView? item) => throw new System.NotImplementedException();
            protected internal override PersonView ToView(Person? entity) => throw new System.NotImplementedException();
        }
        protected override BasePage<PersonView, Person, PersonsRepo> CreateObj() {
            var db = GetRepo.Instance<NullamDb>();
            var repo = new PersonsRepo(db);
            return new TestClass(repo);
        }
        private readonly BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
        [TestMethod] public void RepoTest() => IsProperty<PersonsRepo>();
        [TestMethod] public void ItemTest() => IsProperty<PersonView>();
        [TestMethod] public void ItemsTest() => IsProperty<IList<PersonView>>();
        [TestMethod] public void ItemIdTest() => IsReadOnly<string>();
        [TestMethod] public void TokenTest() => IsReadOnly<string>();
        [TestMethod] public void ErrorMessageTest() => IsProperty<string?>();

        [TestMethod] public void ToViewTest() => IsAbstractMethod(nameof(Obj.ToView), allFlags, typeof(Person));
        [TestMethod] public void ToObjectTest() => IsAbstractMethod(nameof(Obj.ToObject), allFlags, typeof(PersonView));
        [TestMethod] public void RedirectToIndexTest() => IsAbstractMethod(nameof(Obj.RedirectToIndex), allFlags);
        [TestMethod] public void RedirectToDeleteTest() => IsAbstractMethod(nameof(Obj.RedirectToDelete), allFlags, typeof(string));
        [TestMethod] public void RedirectToEditTest() => IsAbstractMethod(nameof(Obj.RedirectToEdit), allFlags, typeof(PersonView));
        [TestMethod] public void GetCreateTest() => IsAbstractMethod(nameof(Obj.GetCreate), allFlags);
        [TestMethod] public void PostCreateAsyncTest() => IsAbstractMethod(nameof(Obj.PostCreateAsync), allFlags);
        [TestMethod] public void GetDetailsAsyncTest() => IsAbstractMethod(nameof(Obj.GetDetailsAsync), allFlags, typeof(string));
        [TestMethod] public void GetDeleteAsyncTest() => IsAbstractMethod(nameof(Obj.GetDeleteAsync), allFlags, typeof(string));
        [TestMethod] public void PostDeleteAsyncTest() => IsAbstractMethod(nameof(Obj.PostDeleteAsync), allFlags, typeof(string), typeof(string)); 
        [TestMethod] public void GetEditAsyncTest() => IsAbstractMethod(nameof(Obj.GetEditAsync), allFlags, typeof(string));
        [TestMethod] public void PostEditAsyncTest() => IsAbstractMethod(nameof(Obj.PostEditAsync), allFlags);
        [TestMethod] public void GetIndexAsyncTest() => IsAbstractMethod(nameof(Obj.GetIndexAsync), allFlags);

        [TestMethod] public void SetAttributesTest() => IsAbstractMethod(nameof(Obj.SetAttributes), allFlags, typeof(int), typeof(string), typeof(string));
        [TestMethod] public void RemoveKey() => IsInconclusive();

        [TestMethod] public void OnGetCreate() => IsInconclusive();
        [TestMethod] public void OnPostCreateAsync() => IsInconclusive();
        [TestMethod] public void Perform() => IsInconclusive();
        [TestMethod] public void OnGetDeleteAsync() => IsInconclusive();
        [TestMethod] public void OnPostDeleteAsync() => IsInconclusive();
        [TestMethod] public void OnGetEditAsync() => IsInconclusive();
        [TestMethod] public void OnPostEditAsync() => IsInconclusive();
        [TestMethod] public void OnGetIndexAsync() => IsInconclusive();
        [TestMethod] public void OnGetDetailsAsync() => IsInconclusive();
    }
}

