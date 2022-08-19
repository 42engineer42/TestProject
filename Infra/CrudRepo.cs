using Microsoft.EntityFrameworkCore;
using Nullam.Data;
using Nullam.Domain;

namespace Nullam.Infra {
    public abstract class CrudRepo<TDomain, TData> : BaseRepo<TDomain, TData> 
        where TDomain : BaseEntity<TData>, new() where TData : BaseData, new() {
        protected CrudRepo(DbContext? context, DbSet<TData>? set) : base(context, set) { }
        public override bool Add(TDomain obj) => AddAsync(obj).GetAwaiter().GetResult();
        public override bool Delete(string id) => DeleteAsync(id).GetAwaiter().GetResult();
        public override List<TDomain> Get() => GetAsync().GetAwaiter().GetResult();
        public override List<TDomain> GetAll(Func<TDomain, dynamic>? orderBy = null) {
            List<TDomain> domains = new();
            if (Set is null) return domains;
            foreach (TData data in Set) domains.Add(ToDomain(data));
            return (orderBy is null) ? domains : domains.OrderBy(orderBy).ToList();
        }
        public override TDomain Get(string id) => GetAsync(id).GetAwaiter().GetResult();
        public override bool Update(TDomain obj) => UpdateAsync(obj).GetAwaiter().GetResult();
        public override async Task<bool> AddAsync(TDomain obj) {
            TData data = obj.Data;
            try {
                _ = (Set is null) ? null : await Set.AddAsync(data);
                _ = (Db is null) ? 0 : await Db.SaveChangesAsync();
                return true;
            }
            catch { return false; }
        }
        public override async Task<bool> DeleteAsync(string id) {
            try {
                TData? data = (Set is null) ? null : await Set.FindAsync(id);
                if (data == null) return false;
                _ = Set?.Remove(data);
                _ = (Db is null) ? 0 : await Db.SaveChangesAsync();
                return true;
            }
            catch { return false; }
        }
        public override async Task<List<TDomain>> GetAsync() {
            try {
                IQueryable<TData> query = CreateSql();
                List<TData> list = await CrudRepo<TDomain, TData>.RunSql(query);
                List<TDomain> items = new();
                foreach (TData d in list) items.Add(ToDomain(d));
                return items;
            }
            catch { return new List<TDomain>(); }
        }
        internal static async Task<List<TData>> RunSql(IQueryable<TData> query) => await query.AsNoTracking().ToListAsync();
        protected internal virtual IQueryable<TData> CreateSql() => from s in Set select s;
        public override async Task<TDomain> GetAsync(string id) {
            try {
                if (id == null) return new TDomain();
                TData? data = (Set is null) ? null : await Set.FirstOrDefaultAsync(x => x.Id == id);
                return data == null ? new TDomain() : ToDomain(data);
            }
            catch { return new TDomain(); }
        }
        public override async Task<bool> UpdateAsync(TDomain obj) {
            try {
                if(Db == null) return false;
                Db.ChangeTracker.Clear();
                TData data = obj.Data;
                Db.Attach(data).State = EntityState.Modified;
                _ = await Db.SaveChangesAsync();
                return true;
            }
            catch { return false; }
        }
        protected internal abstract TDomain ToDomain(TData d);
    }
}

