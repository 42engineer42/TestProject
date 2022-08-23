using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullam.Aids;
using Nullam.Data.Party;
using Nullam.Domain;
using Nullam.Domain.Party;
using Nullam.Infra;

namespace Nullam.Tests.Infra {
    [TestClass]
    public class FilteredRepoTests
        : AbstractClassTests<FilteredRepo<Person, PersonData>, CrudRepo<Person, PersonData>> {
        private class TestClass : FilteredRepo<Person, PersonData> {
            public TestClass(DbContext? context, DbSet<PersonData>? set) : base(context, set) { }
            protected internal override Person ToDomain(PersonData d) => new(d);
        }
        protected override FilteredRepo<Person, PersonData> CreateObj() {
            NullamDb? db = GetRepo.Instance<NullamDb>();
            DbSet<PersonData>? set = db?.Persons;
            return new TestClass(db, set);
        }
        [TestMethod] public void CurrentFilterTest() => IsProperty<string>();

        [DataRow(true)]
        [DataRow(false)]
        [TestMethod] public void CreateSqlTest(bool hasCurrentFilter) {
            Obj.CurrentFilter = hasCurrentFilter ? GetRandom.String() : null;
            IQueryable<PersonData> q1 = Obj.CreateSql();
            IQueryable<PersonData> q2 = Obj.AddFilter(q1);
            AreEqual(q1, q2);
            string s = q1.Expression.ToString();
            IsTrue(s.EndsWith(".Select(s => s)"));
        }
    }
}
