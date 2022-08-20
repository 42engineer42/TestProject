using System.Reflection;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Nullam.Aids;
using Nullam.Domain;
using Nullam.Facade;

namespace Nullam.Pages {
    public abstract class CrudPage<TView, TEntity, TRepo> : BasePage<TView, TEntity, TRepo>
        where TView : BaseView, new() 
        where TEntity : BaseEntity
        where TRepo : ICrudRepo<TEntity> {
        private string homePageUrl = "https://localhost:7254/Events?handler=Index";
        protected CrudPage(TRepo r) : base(r) { }
        protected override IActionResult GetCreate() => Page();
        protected IActionResult ItemPage() => Item == null ? NotFound() : Page();
        protected virtual async Task<IActionResult> GetItemPage(string id) {
            Item = await GetItem(id);
            return Item == null ? NotFound() : Page();
        }
        protected override async Task<IActionResult> GetDetailsAsync(string id) => await GetItemPage(id);
        protected override async Task<IActionResult> GetDeleteAsync(string id) {
            ErrorMessage = TempData["Error"] as string;
            return await GetItemPage(id);
        }
        protected override async Task<IActionResult> GetEditAsync(string id) {
            string? item = TempData["Item"] as string;
            TView? view = null;
            if (item is not null) view = JsonSerializer.Deserialize<TView>(item);
            if (view is null) return await GetItemPage(id);
            return await GetEditAsync(view);
        }
        protected async Task<IActionResult> GetEditAsync(TView v) {
            Item = await GetItem(v.Id);
            ModelState.AddModelError(string.Empty,
                "The record you attempted to edit was modified by another user after you. The "
                + "edit operation was canceled and the current values in the database "
                + "have been displayed. If you still want to edit this record, click "
                + "the Save button again.");
            foreach (PropertyInfo p in Item.GetType().GetProperties()) {
                string name = p.Name;
                string? currentValue = p.GetValue(Item)?.ToString();
                string? clientValue = v?.GetType()?.GetProperty(name)?.GetValue(v)?.ToString();
                if (currentValue != clientValue)
                    ModelState.AddModelError(
                        $"{nameof(Item)}.{name}",
                        $"Your value: {clientValue}");
            }
            return ItemPage();
        }
        protected override async Task<IActionResult> PostCreateAsync() {
            if (!ModelState.IsValid) return Page();
            _ = await Repo.AddAsync(ToObject(Item));
            return Redirect(homePageUrl);
        }
        protected override async Task<IActionResult> PostDeleteAsync(string id, string? token = null) {
            if (id == null) return Redirect(homePageUrl);
            TView view = await GetItem(id);
            if (ConcurrencyToken.ToStr(view.Token) == ConcurrencyToken.ToStr()) return Redirect(homePageUrl);
            string viewToken = ConcurrencyToken.ToStr(view.Token);
            if (viewToken != token) return RedirectToDelete(id);

            _ = await Repo.DeleteAsync(id);
            return Redirect(homePageUrl);
        }
        protected override async Task<IActionResult> PostEditAsync() {
            TEntity entity = Repo.Get(Item.Id);
            if (ConcurrencyToken.ToStr(entity.Token) == ConcurrencyToken.ToStr()) {
                ModelState.AddModelError(string.Empty, "Unable to save. The item was deleted by another user.");
                return Page();
            }
            string entityToken = ConcurrencyToken.ToStr(entity.Token);
            string itemToken = ConcurrencyToken.ToStr(Item.Token);
            if (entityToken != itemToken) return RedirectToEdit(Item);

            if (!ModelState.IsValid) return Page();
            TEntity obj = ToObject(Item);
            bool updated = await Repo.UpdateAsync(obj);
            return !updated ? NotFound() : Redirect(homePageUrl);
        }
        protected override async Task<IActionResult> GetIndexAsync() {
            List<TEntity> list = await Repo.GetAsync();
            Items = new List<TView>();
            foreach (TEntity obj in list) {
                TView view = ToView(obj);
                Items.Add(view);
            }
            return Page();
        }
        private async Task<TView> GetItem(string id)
            => ToView(await Repo.GetAsync(id));
    }
}
