using Microsoft.EntityFrameworkCore;
using Nullam.Data;
using Nullam.Domain;

namespace Nullam.Infra {
    public abstract class BaseRepo<TDomain, TData> : IBaseRepo<TDomain>
        where TDomain : BaseEntity<TData>, new() where TData : BaseData, new() {
        protected internal DbContext? Db { get; }
        protected internal DbSet<TData>? Set { get; }
        protected BaseRepo(DbContext? context, DbSet<TData>? set) {
            Db = context;
            Set = set;
        }
        internal void Clear() {
            Set?.RemoveRange(Set);
            Db?.SaveChanges();
        }
        public abstract bool Add(TDomain obj);
        public abstract Task<bool> AddAsync(TDomain obj);
        public abstract bool Delete(string id);
        public abstract Task<bool> DeleteAsync(string id);
        public abstract List<TDomain> Get();
        public abstract List<TDomain> GetAll(Func<TDomain, dynamic>? orderBy = null);
        public abstract TDomain Get(string id);
        public abstract Task<List<TDomain>> GetAsync();
        public abstract Task<TDomain> GetAsync(string id);
        public abstract bool Update(TDomain obj);
        public abstract Task<bool> UpdateAsync(TDomain obj);
    }
}
