using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nullam.Aids;
using Nullam.Domain;
using Nullam.Facade;

namespace Nullam.Pages {
    public abstract class BasePage<TView, TEntity, TRepo> : PageModel 
        where TView : BaseView, new() 
        where TEntity : BaseEntity
        where TRepo : IBaseRepo<TEntity>{
        protected internal TRepo Repo { get; set; }
        public BasePage(TRepo r) => Repo = r;
        [BindProperty] public TView Item { get; set; } = new TView();
        public IList<TView> Items { get; set; } = new List<TView>();
        public string ItemId => Item?.Id ?? string.Empty;
        public string Token => ConcurrencyToken.ToStr(Item?.Token);
        public string ErrorMessage { get; set; }
        protected internal abstract TEntity ToObject(TView? item);
        protected internal abstract TView ToView(TEntity? entity);
        protected internal abstract IActionResult RedirectToIndex();
        protected internal abstract IActionResult RedirectToDelete(string id);
        protected internal abstract IActionResult RedirectToEdit(TView v);
        
        protected internal abstract void SetAttributes(int index, string? filter, string? order);
        protected internal abstract IActionResult GetCreate();
        protected internal abstract Task<IActionResult> GetIndexAsync(); 
        protected internal abstract Task<IActionResult> PostCreateAsync();
        protected internal abstract Task<IActionResult> GetDetailsAsync(string id);
        protected internal abstract Task<IActionResult> GetDeleteAsync(string id);
        protected internal abstract Task<IActionResult> PostDeleteAsync(string id, string? token = null);
        protected internal abstract Task<IActionResult> GetEditAsync(string id);
        protected internal abstract Task<IActionResult> PostEditAsync();
        protected async virtual Task<IActionResult> Perform(Func<Task<IActionResult>>f, int index, string? filter, string? order, bool removeKeys = false) {
            SetAttributes(index, filter, order);
            if(removeKeys) RemoveKey(nameof(filter), nameof(order));
            return await f();
        }
        internal virtual void RemoveKey(params string[] keys) {
            foreach (string key in keys ?? Array.Empty<string>())
                _ = Safe.Run(() => ModelState.Remove(key));
        }
        public IActionResult OnGetCreate(int index = 0, string? filter = null, string? order = null) {
            SetAttributes(index, filter, order);
            return GetCreate();
        }
        public virtual async Task<IActionResult> OnPostCreateAsync(int index = 0, string? filter = null, string? order = null) 
            => await Perform(() => PostCreateAsync(), index, filter, order, true); 
        public async Task<IActionResult> OnGetDetailsAsync(string id, int index = 0, string? filter = null, string? order = null)
            => await Perform(() => GetDetailsAsync(id), index, filter, order);
        public virtual async Task<IActionResult> OnGetDeleteAsync(string id, int index = 0, string? filter = null, string? order = null)
            => await Perform(() => GetDeleteAsync(id), index, filter, order);
        public virtual async Task<IActionResult> OnPostDeleteAsync(string id, int index = 0, string? filter = null, string? order = null, string? token = null)
            => await Perform(() => PostDeleteAsync(id, token), index, filter, order);
        public virtual async Task<IActionResult> OnGetEditAsync(string id, int index = 0, string? filter = null, string? order = null)
            => await Perform(() => GetEditAsync(id), index, filter, order);
        public virtual async Task<IActionResult> OnPostEditAsync(int index = 0, string? filter = null, string? order = null)
            => await Perform(() => PostEditAsync(), index, filter, order, true);
        public async Task<IActionResult> OnGetIndexAsync(int index = 0, string? filter = null, string? order = null)
            => await Perform(() => GetIndexAsync(), index, filter, order);
    }
}
