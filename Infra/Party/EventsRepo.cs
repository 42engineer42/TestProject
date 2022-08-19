
using Nullam.Data.Party;
using Nullam.Domain.Party;

namespace Nullam.Infra.Party {
    public sealed class EventsRepo : Repo<Event, EventData>, IEventsRepo {
        public EventsRepo(NullamDb? db) : base(db, db?.Events) { }
        protected internal override Event ToDomain(EventData d) => new(d);
        internal override IQueryable<EventData> AddFilter(IQueryable<EventData> q) {
            string? y = CurrentFilter;
            return string.IsNullOrWhiteSpace(y) ? q : q.Where(
                x => x.Id.Contains(y)
                     || x.Name.Contains(y)
                     || x.Place.Contains(y)
                     || x.Date.ToString().Contains(y)
                     || x.AdditionalInfo.Contains(y)
                );
        }
    }
}
