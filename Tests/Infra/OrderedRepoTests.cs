using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullam.Aids;
using Nullam.Data.Party;
using Nullam.Domain;
using Nullam.Domain.Party;
using Nullam.Infra;

namespace Nullam.Tests.Infra {
    [TestClass] public class OrderedRepoTests
        : AbstractClassTests<OrderedRepo<Person, PersonData>, FilteredRepo<Person, PersonData>> {
        private class TestClass : OrderedRepo<Person, PersonData> {
            public TestClass(DbContext? context, DbSet<PersonData>? set) : base(context, set) { }
            protected internal override Person ToDomain(PersonData d) => new(d);
        }
        protected override OrderedRepo<Person, PersonData> CreateObj() {
            NullamDb? db = GetRepo.Instance<NullamDb>();
            DbSet<PersonData>? set = db?.Persons;
            return new TestClass(db, set);
        }
        [TestMethod] public void CurrentOrderTest() => IsProperty<string>();
        [TestMethod] public void DescendingStringTest() => AreEqual("_desc", TestClass.DescendingString);

        [DataRow(null, true)]
        [DataRow(null, false)]
        [DataRow(nameof(Person.Id), true)]
        [DataRow(nameof(Person.Id), false)]
        [DataRow(nameof(Person.FirstName), true)]
        [DataRow(nameof(Person.FirstName), false)]
        [DataRow(nameof(Person.LastName), true)]
        [DataRow(nameof(Person.LastName), false)]
        [DataRow(nameof(Person.PayingType), true)]
        [DataRow(nameof(Person.PayingType), false)]
        [DataRow(nameof(Person.PersonalCode), true)]
        [DataRow(nameof(Person.PersonalCode), false)]
        [TestMethod] public void CreateSqlTest(string? str, bool isDescending) {
            Obj.CurrentOrder = (str is null) ? str : isDescending ? str + TestClass.DescendingString : str;
            IQueryable<PersonData> q = Obj.CreateSql();
            string? actual = q.Expression.ToString();
            if (str is null) IsTrue(actual.EndsWith(".Select(s => s)"));
            else if (isDescending) IsTrue(actual.EndsWith(
                $".Select(s => s).OrderByDescending(x => Convert(x.{str}, Object))"));
            else IsTrue(actual.EndsWith(
                $".Select(s => s).OrderBy(x => Convert(x.{str}, Object))"));
        }

        [DataRow(true, true)]
        [DataRow(false, true)]
        [DataRow(true, false)]
        [DataRow(false, false)]
        [TestMethod] public void SortOrderTest(bool isDescending, bool isSame) {
            string str = GetRandom.String();
            string c = isSame ? str : GetRandom.String();
            Obj.CurrentOrder = isDescending ? c + TestClass.DescendingString : c;
            string actual = Obj.SortOrder(str);
            string sDes = str + TestClass.DescendingString;
            string expected = isSame ? (isDescending ? str : sDes) : sDes;
            AreEqual(expected, actual);
        }
    }
}
