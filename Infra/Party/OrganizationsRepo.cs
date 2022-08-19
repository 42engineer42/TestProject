using Nullam.Data.Party;
using Nullam.Domain.Party;

namespace Nullam.Infra.Party {
    public sealed class OrganizationsRepo : Repo<Organization, OrganizationData>, IOrganizationsRepo {
        public OrganizationsRepo(NullamDb? db) : base(db, db?.Organizations) { }
        protected internal override Organization ToDomain(OrganizationData d) => new(d);
        internal override IQueryable<OrganizationData> AddFilter(IQueryable<OrganizationData> q) {
            string? y = CurrentFilter;
            return string.IsNullOrWhiteSpace(y) ? q : q.Where(
                x => x.Id.Contains(y)
                     || x.Name.Contains(y)
                     || x.OrganizationCode.Contains(y)
                     || x.ParticipantsAmount.ToString().Contains(y)
                     || x.AdditionalInfo.Contains(y)
                     || x.PayingType.ToString().Contains(y)
                );
        }
    }
}