using Nullam.Aids;
using Nullam.Data;
using Nullam.Domain;

namespace Nullam.Facade {
    public abstract class BaseViewFactory<TView, TEntity, TData>
    where TView : class, new()
    where TData : BaseData, new()
    where TEntity : BaseEntity<TData> {
        protected abstract TEntity ToEntity(TData d);
        protected virtual void Copying(object? from, object? to) => Copy.Properties(from, to);
        public virtual TEntity Create(TView? v) {
            TData d = new TData();
            Copying(v, d);
            return ToEntity(d);
        }
        public virtual TView Create(TEntity? e) {
            TData? d = e?.Data;
            TView v = new TView();
            Copying(d, v);
            return v;
        }
    }
}
