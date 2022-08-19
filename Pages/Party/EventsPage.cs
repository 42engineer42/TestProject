
using System.Linq.Expressions;
using Nullam.Domain.Party;
using Nullam.Facade.Party;

namespace Nullam.Pages.Party {
    public class EventsPage : PagedPage<EventView, Event, IEventsRepo> {
        public EventsPage(IEventsRepo r) : base(r) { }
        protected override Event ToObject(EventView? item) => new EventViewFactory().Create(item);
        protected override EventView ToView(Event? entity) => new EventViewFactory().Create(entity);
        public override string[] IndexColumns { get; } = new[] {
            nameof(EventView.Name),
            nameof(EventView.Date)
        };
        public override string[] ColumnsForPerson { get; } = new[] {
            nameof(PersonView.FirstName),
            nameof(PersonView.LastName),
            nameof(PersonView.PersonalCode)
        };

        public override string[] ColumnsForOrganization { get; } = new[] {
            nameof(OrganizationView.Name),
            nameof(OrganizationView.OrganizationCode)
        };

        public override string TitleForAddingNewObject { get; } = "Ürituse lisamine";
        public override string TitleForDetailsView { get; } = "Osavõtjad";

        public string TitleForUpcomingEvents { get; } = "Tulevased üritused";
        public string TitleForPastEvents { get; } = "Möödunud üritused";

        public Lazy<List<Person?>> Persons => ToObject(Item).Persons;
        public Lazy<List<Organization?>> Organizations => ToObject(Item).Organizations;

        public IList<EventView> GetPastEvents(IList<EventView> items) {
            IList<EventView> filteredItems = new List<EventView>();
            foreach (var item in items) {
                if (item.Date < DateTime.Now) {
                    filteredItems.Add(item);
                }
            }
            return filteredItems;
        }

        public IList<EventView> GetUpcomingEvents(IList<EventView> items) {
            IList<EventView> filteredItems = new List<EventView>();
            foreach (var item in items) {
                if (item.Date > DateTime.Now) {
                    filteredItems.Add(item);
                }
            }
            return filteredItems;
        }
    }
}
