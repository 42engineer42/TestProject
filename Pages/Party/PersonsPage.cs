using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nullam.Aids;
using Nullam.Data;
using Nullam.Domain.Party;
using Nullam.Facade.Party;

namespace Nullam.Pages.Party {
    public class PersonsPage : PagedPage<PersonView, Person, IPersonsRepo> {
        private readonly IEventsRepo events;
        public PersonsPage(IPersonsRepo r, IEventsRepo e) : base(r) => events = e;
        protected override Person ToObject(PersonView? item) => new PersonViewFactory().Create(item);
        protected override PersonView ToView(Person? entity) => new PersonViewFactory().Create(entity);
        public override string[] IndexColumns { get; } = new[] {
            nameof(PersonView.FirstName),
            nameof(PersonView.LastName),
            nameof(PersonView.PersonalCode),
            nameof(PersonView.PayingType),
            nameof(PersonView.AdditionalInfo)
        };
        public override string TitleForDetailsView { get; } = "Osavõtja info";
        public override string TitleForAddingNewObject { get; } = "Osavõtjate lisamine";

        public IEnumerable<SelectListItem> PayingTypes
            => Enum.GetValues<PayingType>()?.Select(type => new SelectListItem(type.GetDescription(), type.ToString())) ?? new List<SelectListItem>();

        public IEnumerable<SelectListItem> Events => GetUpcomingEvents();

        public string PayingTypeDescription(PayingType? type)
            => (type ?? PayingType.Cash).GetDescription();

        public string EventDescriptor(string? eventId = null)
            => Events?.FirstOrDefault(e => e.Value == (eventId ?? string.Empty))?.Text ?? "Pole valitud";

        public override object? GetValue<T>(string name, T v) {
            object? obj = base.GetValue(name, v);
            if (name == nameof(OrganizationView.PayingType)) return PayingTypeDescription((PayingType) obj);
            if (name == nameof(OrganizationView.EventId)) return EventDescriptor(obj as string);
            return obj;
        }

        private IEnumerable<SelectListItem> GetUpcomingEvents() {
            var allEvents = events.GetAll(e => e.ToString());
            var upcomingEvents = new List<Event>();
            foreach (var item in allEvents) {
                if (item.Date > DateTime.Now) {
                    upcomingEvents.Add(item);
                }
            }
            return upcomingEvents.Select(e => new SelectListItem(e.ToString(), e.Id)) ?? new List<SelectListItem>();
        }
    }
}