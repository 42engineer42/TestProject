using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullam.Aids;
using Nullam.Data.Party;
using Nullam.Domain;
using Nullam.Domain.Party;
using Nullam.Infra;

namespace Nullam.Tests.Infra {
    [TestClass] public class BaseRepoTests
        : AbstractClassTests<BaseRepo<Person, PersonData>, object> {
        private class TestClass : BaseRepo<Person, PersonData> {
            public TestClass(DbContext? context, DbSet<PersonData>? set) : base(context, set) { }
            public override bool Add(Person obj) => throw new NotImplementedException();
            public override Task<bool> AddAsync(Person obj) => throw new NotImplementedException();
            public override bool Delete(string id) => throw new NotImplementedException();
            public override Task<bool> DeleteAsync(string id) => throw new NotImplementedException();
            public override List<Person> Get() => throw new NotImplementedException();
            public override Person Get(string id) => throw new NotImplementedException();
            public override List<Person> GetAll(Func<Person, dynamic>? orderBy = null)
                => throw new NotImplementedException();
            public override Task<List<Person>> GetAsync() => throw new NotImplementedException();
            public override Task<Person> GetAsync(string id) => throw new NotImplementedException();
            public override bool Update(Person obj) => throw new NotImplementedException();
            public override Task<bool> UpdateAsync(Person obj) => throw new NotImplementedException();
        }
        protected override BaseRepo<Person, PersonData> CreateObj() => new TestClass(null, null);
        [TestMethod] public void DbTest() => IsReadOnly<DbContext?>();
        [TestMethod] public void SetTest() => IsReadOnly<DbSet<PersonData>?>();
        [TestMethod] public void BaseRepoTest() {
            NullamDb? db = GetRepo.Instance<NullamDb>();
            DbSet<PersonData>? set = db?.Persons;
            IsNotNull(set);
            Obj = new TestClass(db, set);
            AreEqual(db, Obj.Db);
            AreEqual(set, Obj.Set);
        }
        [TestMethod] public async Task ClearTest() {
            BaseRepoTest();
            int cnt = GetRandom.Int32(5, 30);
            DbContext? db = Obj.Db;
            IsNotNull(db);
            DbSet<PersonData>? set = Obj.Set;
            IsNotNull(set);
            for (int i = 0; i < cnt; i++) set.Add(GetRandom.Value<PersonData>());
            AreEqual(0, await set.CountAsync());
            db.SaveChanges();
            AreEqual(cnt, await set.CountAsync());
            Obj.Clear();
            AreEqual(0, await set.CountAsync());
        }
        [TestMethod] public void AddTest() => IsAbstractMethod(nameof(Obj.Add), typeof(Person));
        [TestMethod] public void AddAsyncTest() => IsAbstractMethod(nameof(Obj.AddAsync), typeof(Person));
        [TestMethod] public void DeleteTest() => IsAbstractMethod(nameof(Obj.Delete), typeof(string));
        [TestMethod] public void DeleteAsyncTest() => IsAbstractMethod(nameof(Obj.DeleteAsync), typeof(string));
        [TestMethod] public void GetTest() => IsAbstractMethod(nameof(Obj.Get), typeof(string));
        [TestMethod] public void GetAllTest() => IsAbstractMethod(nameof(Obj.GetAll), typeof(Func<Person, dynamic>));
        [TestMethod] public void GetListTest() => IsAbstractMethod(nameof(Obj.Get));
        [TestMethod] public void GetAsyncTest() => IsAbstractMethod(nameof(Obj.GetAsync), typeof(string));
        [TestMethod] public void GetListAsyncTest() => IsAbstractMethod(nameof(Obj.GetAsync));
        [TestMethod] public void UpdateTest() => IsAbstractMethod(nameof(Obj.Update), typeof(Person));
        [TestMethod] public void UpdateAsyncTest() => IsAbstractMethod(nameof(Obj.UpdateAsync), typeof(Person));
    }
}
