using Nullam.Data.Party;
using Nullam.Domain.Party;

namespace Nullam.Infra.Party {
    public sealed class PersonsRepo : Repo<Person, PersonData>, IPersonsRepo {
        public PersonsRepo(NullamDb? db) : base(db, db?.Persons) { }
        protected internal override Person ToDomain(PersonData d) => new(d);
        internal override IQueryable<PersonData> AddFilter(IQueryable<PersonData> q) {
            string? y = CurrentFilter;
            return string.IsNullOrWhiteSpace(y) ? q : q.Where(
                x => x.Id.Contains(y)
                     || x.FirstName.Contains(y)
                     || x.LastName.Contains(y)
                     || x.PayingType.ToString().Contains(y)
                     || x.AdditionalInfo.Contains(y)
                     || x.PersonalCode.Contains(y)
                );
        }
    }
}