using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullam.Data.Party;
using Nullam.Domain;
using Nullam.Domain.Party;
using Nullam.Infra;
using Nullam.Infra.Party;

namespace Nullam.Tests.Infra.Party {
    [TestClass] public class PersonsRepoTests : SealedRepoTests<PersonsRepo, Repo<Person, PersonData>, Person, PersonData> {
        protected override PersonsRepo CreateObj() => new(GetRepo.Instance<NullamDb>());
        protected override object? GetSet(NullamDb db) => db.Persons;
    }
}