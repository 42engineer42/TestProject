using System.ComponentModel;
using System.Reflection;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Nullam.Aids;
using Nullam.Domain;
using Nullam.Facade;

namespace Nullam.Pages {
    public abstract class PagedPage<TView, TEntity, TRepo> : OrderedPage<TView, TEntity, TRepo>, IPageModel, IIndexModel<TView>
        where TView : BaseView, new()
        where TEntity : BaseEntity
        where TRepo : IPagedRepo<TEntity> {
        protected PagedPage(TRepo r) : base(r) { }
        public int PageIndex {
            get => Repo.PageIndex;
            set => Repo.PageIndex = value;
        }
        protected override void SetAttributes(int pageIndex, string? currentFilter, string? sortOrder) {
            PageIndex = pageIndex;
            CurrentFilter = currentFilter;
            CurrentOrder = sortOrder;
        }
        public int TotalPages => Repo.TotalPages;
        public bool HasNextPage  => Repo.HasNextPage;
        public bool HasPreviousPage  => Repo.HasPreviousPage;
        public virtual string[] IndexColumns => Array.Empty<string>();
        public virtual string[] ColumnsForPerson => Array.Empty<string>();
        public virtual string[] ColumnsForOrganization => Array.Empty<string>();
        public virtual string TitleForAddingNewObject => string.Empty;
        public virtual string TitleForDetailsView => string.Empty;
        protected override IActionResult RedirectToIndex() => RedirectToPage("./Index", "Index", new {
            pageIndex = PageIndex,
            currentFilter = CurrentFilter,
            sortOrder = CurrentOrder} 
        );
        protected internal override IActionResult RedirectToEdit(TView v) {
            TempData["Item"] = JsonSerializer.Serialize(v);
            return RedirectToPage("./Edit", "Edit",
            new {
                id = v.Id,
                pageIndex = PageIndex,
                currentFilter = CurrentFilter,
                sortOrder = CurrentOrder
            });
        }
        protected internal override IActionResult RedirectToDelete(string id) {
            TempData["Error"] = "The record you attempted to delete "
                  + "was modified by another user after you selected delete. "
                  + "The delete operation was canceled and the current values in the "
                  + "database have been displayed. If you still want to delete this "
                  + "record, click the Delete button again.";

            return RedirectToPage("./Delete", "Delete",
            new {
                id,
                pageIndex = PageIndex,
                currentFilter = CurrentFilter,
                sortOrder = CurrentOrder
            });
        } 
        public virtual object? GetValue<T>(string name, T v)
            => Safe.Run(() => {
                PropertyInfo? propertyInfo = v?.GetType()?.GetProperty(name);
                return propertyInfo?.GetValue(v);
            }, null); 
        public string? GetDisplayName<T>(string propertyName) 
            => Safe.Run(() => {
                PropertyInfo? propertyInfo = typeof(T).GetProperty(propertyName);
                object[]? obj = propertyInfo ? .GetCustomAttributes(typeof(DisplayNameAttribute), true);
                return obj?.Cast<DisplayNameAttribute>().Single().DisplayName;
            }, propertyName);

       
    }
}
