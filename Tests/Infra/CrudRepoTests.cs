using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullam.Aids;
using Nullam.Data.Party;
using Nullam.Domain;
using Nullam.Domain.Party;
using Nullam.Infra;

namespace Nullam.Tests.Infra {
    [TestClass] public class CrudRepoTests
        : AbstractClassTests<CrudRepo<Person, PersonData>, BaseRepo<Person, PersonData>> {
        private NullamDb? db;
        private DbSet<PersonData>? set;
        private int count;
        private PersonData? data;
        private Person? obj;
        private class TestClass : CrudRepo<Person, PersonData> {
            public TestClass(DbContext? context, DbSet<PersonData>? set) : base(context, set) { }
            protected internal override Person ToDomain(PersonData d) => new(d);
        }
        protected override CrudRepo<Person, PersonData> CreateObj() {
            db = GetRepo.Instance<NullamDb>();
            set = db?.Persons;
            IsNotNull(set);
            return new TestClass(db, set);
        }
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            InitSet();
            InitCharacter();
        }
        private void InitCharacter() {
            data = GetRandom.Value<PersonData>();
            IsNotNull(data);
            obj = new Person(data);
            Person x = Obj.Get(data.Id);
            IsNotNull(x);
            AreNotEqual(data.Id, x.Id);
        }
        private void InitSet() {
            count = GetRandom.Int32(5, 15);
            for (int i = 0; i < count; i++) set?.Add(GetRandom.Value<PersonData>());
            _ = (db?.SaveChanges());
        }
        [TestMethod] public async Task AddTest() {
            IsNotNull(obj);
            IsNotNull(set);
            _ = Obj?.Add(obj);
            AreEqual(count + 1, await set.CountAsync());
        }
        [TestMethod] public async Task AddAsyncTest() {
            IsNotNull(obj);
            IsNotNull(set);
            _ = Obj?.AddAsync(obj);
            AreEqual(count + 1, await set.CountAsync());
        }
        [TestMethod] public async Task DeleteTest() {
            IsNotNull(data);
            await GetTest();
            _ = Obj.Delete(data.Id);
            Person entity = Obj.Get(data.Id);
            IsNotNull(entity);
            AreNotEqual(data.Id, entity.Id);
        }
        [TestMethod] public async Task DeleteAsyncTest() {
            IsNotNull(data);
            await GetTest();
            _ = Obj.DeleteAsync(data.Id);
            Person entity = Obj.Get(data.Id);
            IsNotNull(entity);
            AreNotEqual(data.Id, entity.Id);
        }
        [TestMethod] public async Task GetTest() {
            IsNotNull(data);
            await AddTest();
            Person entity = Obj.Get(data.Id);
            ArePropertiesEqual(data, entity.Data);
        }

        [DataRow(nameof(Person.Id))]
        [DataRow(nameof(Person.FirstName))]
        [DataRow(nameof(Person.LastName))]
        [DataRow(nameof(Person.PersonalCode))]
        [DataRow(nameof(Person.PayingType))]
        [DataRow(null)]
        [TestMethod] public void GetAllTest(string s) {
            Func<Person, dynamic>? orderBy = null;

            if (s is nameof(Person.Id)) orderBy = x => x.Id;
            else if (s is nameof(Person.FirstName)) orderBy = x => x.FirstName;
            else if (s is nameof(Person.LastName)) orderBy = x => x.LastName;
            else if (s is nameof(Person.PersonalCode)) orderBy = x => x.PersonalCode;
            else if (s is nameof(Person.PayingType)) orderBy = x => x.PayingType;

            List<Person> list = Obj.GetAll(orderBy);
            AreEqual(count, list.Count);
            if (orderBy is null) return;

            for (int i = 0; i < list.Count - 1; i++) {
                Person a = list[i];
                Person b = list[i + 1];
                IComparable? aX = orderBy(a) as IComparable;
                IComparable? bX = orderBy(b) as IComparable;
                IsNotNull(aX);
                IsNotNull(bX);
                int r = aX.CompareTo(bX);
                IsTrue(r <= 0);
            }
        }
        [TestMethod] public void GetListTest() {
            List<Person> list = Obj.Get();
            AreEqual(count, list.Count);
        }
        [TestMethod] public async Task GetAsyncTest() {
            IsNotNull(data);
            await AddAsyncTest();
            Person entity = await Obj.GetAsync(data.Id);
            ArePropertiesEqual(data, entity.Data);
        }
        [TestMethod] public async Task GetListAsyncTest() {
            List<Person> list = await Obj.GetAsync();
            AreEqual(count, list.Count);
        }
        [TestMethod] public async Task UpdateTest() {
            await GetTest();
            PersonData? dX = GetRandom.Value<PersonData>() as PersonData;
            IsNotNull(data);
            IsNotNull(dX);
            dX.Id = data.Id;
            Person aX = new(dX);
            _ = Obj.Update(aX);
            Person x = Obj.Get(data.Id);
            ArePropertiesEqual(dX, x.Data);
        }
        [TestMethod] public async Task UpdateAsyncTest() {
            await GetTest();
            PersonData? dX = GetRandom.Value<PersonData>() as PersonData;
            IsNotNull(data);
            IsNotNull(dX);
            dX.Id = data.Id;
            Person aX = new Person(dX);
            _ = await Obj.UpdateAsync(aX);
            Person x = Obj.Get(data.Id);
            ArePropertiesEqual(dX, x.Data);
        }
    }
}
