using Microsoft.EntityFrameworkCore;
using Nullam.Data;
using Nullam.Domain;

namespace Nullam.Infra {
    public abstract class Repo<TDomain, TData> : PagedRepo<TDomain, TData> where TDomain : BaseEntity<TData>, new() where TData : BaseData, new() {
        protected Repo(DbContext? context, DbSet<TData>? set) : base(context,set) { }
    }
}
