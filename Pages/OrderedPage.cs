using System.ComponentModel;
using System.Reflection;
using Nullam.Domain;
using Nullam.Facade;

namespace Nullam.Pages {
    public abstract class OrderedPage<TView, TEntity, TRepo> : FilteredPage<TView, TEntity, TRepo>
        where TView : BaseView, new()
        where TEntity : BaseEntity
        where TRepo : IOrderedRepo<TEntity> {
        protected OrderedPage(TRepo r) : base(r) { }
        public string? CurrentOrder {
            get => FromCurrentOrder(Repo.CurrentOrder);
            set => Repo.CurrentOrder = ToCurrentOrder(value);
        } 
        private static string? FromCurrentOrder(string? value) {
            bool isDesc = value?.Contains("_desc") ?? false;
            string? propertyName = value?.Replace("_desc", string.Empty);
            PropertyInfo? propertyInfo = typeof(TView).GetProperty(propertyName);
            string? displayName = GetDisplayName(propertyInfo);
            return isDesc ? displayName + "_desc" : displayName;
        }
        private static string? GetDisplayName(PropertyInfo? pi) => pi?.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName;
        private static string? ToCurrentOrder(string? value) {
            bool isDesc = value?.Contains("_desc") ?? false;
            string? displayName = value?.Replace("_desc", string.Empty);
            foreach (PropertyInfo propertyInfo in typeof(TView).GetProperties()) {
                if (!IsThisDisplayName(propertyInfo, displayName)) continue;
                return isDesc ? propertyInfo.Name + "_desc" : propertyInfo.Name;
            }
            return value;
        }
        private static bool IsThisDisplayName(PropertyInfo propertyInfo, string? displayName) => GetDisplayName(propertyInfo) == displayName; 
        public string? SortOrder(string displayName) => Repo.SortOrder(ToCurrentOrder(displayName)); 
    }
}
