using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Nullam.Data;
using Nullam.Domain;

namespace Nullam.Infra {
    public abstract class OrderedRepo<TDomain, TData> : FilteredRepo<TDomain, TData>
        where TDomain : BaseEntity<TData>, new() where TData : BaseData, new() {
        protected OrderedRepo(DbContext? context, DbSet<TData>? set) : base(context, set) { }
        public string? CurrentOrder { get; set; }
        public static string DescendingString => "_desc";
        protected internal override IQueryable<TData> CreateSql() => AddSort(base.CreateSql());
        internal IQueryable<TData> AddSort(IQueryable<TData> q) {
            if (string.IsNullOrWhiteSpace(CurrentOrder)) return q;
            Expression<Func<TData, object>>? e = LambdaExpression;
            return e == null ? q
                : IsDescending ? q.OrderByDescending(e)
                : (IQueryable<TData>)q.OrderBy(e);
        }
        internal bool IsDescending => CurrentOrder?.EndsWith(DescendingString) ?? false;
        internal bool IsSameProperty(string s) => !string.IsNullOrEmpty(s) && (CurrentOrder?.StartsWith(s) ?? false);
        internal string PropertyName => CurrentOrder?.Replace(DescendingString, "") ?? "";
        internal PropertyInfo? PropertyInfo => typeof(TData).GetProperty(PropertyName);
        internal Expression<Func<TData, object>>? LambdaExpression {
            get {
                if (PropertyInfo is null) return null;
                ParameterExpression param = Expression.Parameter(typeof(TData), "x");
                MemberExpression property = Expression.Property(param, PropertyInfo);
                UnaryExpression body = Expression.Convert(property, typeof(object));
                return Expression.Lambda<Func<TData, object>>(body, param);
            }
        }
        public string SortOrder(string propertyName) {
            string n = propertyName;
            return !IsSameProperty(n) ? n + DescendingString : IsDescending ? n : n + DescendingString;
        }
    }
}
