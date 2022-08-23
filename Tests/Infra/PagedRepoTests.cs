using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullam.Data.Party;
using Nullam.Domain;
using Nullam.Domain.Party;
using Nullam.Infra;

namespace Nullam.Tests.Infra {
    [TestClass]
    public class PagedRepoTests
        : AbstractClassTests<PagedRepo<Person, PersonData>, OrderedRepo<Person, PersonData>> {
        private class TestClass : PagedRepo<Person, PersonData> {
            public TestClass(DbContext? context, DbSet<PersonData>? set) : base(context, set) { }
            protected internal override Person ToDomain(PersonData d) => new(d);
        }
        protected override PagedRepo<Person, PersonData> CreateObj() {
            NullamDb? db = GetRepo.Instance<NullamDb>();
            DbSet<PersonData>? set = db?.Persons;
            IsNotNull(set);
            return new TestClass(db, set);
        }
        [TestMethod] public void PageIndexTest() => IsProperty<int>();
        [TestMethod] public void HasPreviousPageTest() => IsReadOnly<bool?>();
        [TestMethod] public void PageSizeTest() => IsProperty<int>();
        [TestMethod] public void TotalPagesTest() => IsReadOnly<int>();
        [TestMethod] public void CountPagesTest() => IsReadOnly<double>();
        [TestMethod] public void ItemsCountTest() => IsReadOnly<int>();
        [TestMethod] public void HasNextPageTest() => IsReadOnly<bool>();
    }
}