using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullam.Data.Party;
using Nullam.Domain.Party;
using Nullam.Infra;

namespace Nullam.Tests.Infra {
    [TestClass]
    public class RepoTests : AbstractClassTests<Repo<Person, PersonData>, PagedRepo<Person, PersonData>> {
        private class TestClass : Repo<Person, PersonData> {
            public TestClass(DbContext? context, DbSet<PersonData>? set) : base(context, set) { }
            protected internal override Person ToDomain(PersonData d) => new(d);
        }
        protected override Repo<Person, PersonData> CreateObj() => new TestClass(null, null);
    }
}