using Nullam.Data;
using Nullam.Data.Party;

namespace Nullam.Domain.Party {
    public interface IOrganizationsRepo: IRepo<Organization> {}

    public sealed class Organization : BaseEntity<OrganizationData> {
        public Organization(): this(new()) {}
        public Organization(OrganizationData data): base(data) {}

        public string Name => GetValue(Data?.Name);
        public string OrganizationCode => GetValue(Data?.OrganizationCode);
        public int ParticipantsAmount => GetValue(Data?.ParticipantsAmount);
        public PayingType PayingType => GetValue(Data?.PayingType);
        public string EventId => GetValue(Data?.EventId);
        public Event? Event => GetRepo.Instance<IEventsRepo>()?.Get(EventId);
    }
}
