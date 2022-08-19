using Nullam.Data.Party;

namespace Nullam.Domain.Party {
    public interface IEventsRepo: IRepo<Event> {}
    public sealed class Event : BaseEntity<EventData> {
        public Event(): this(new()) {}
        public Event(EventData data): base(data) {}

        public string Name => GetValue(Data?.Name);
        public DateTime Date => GetValue(Data?.Date);
        public string Place => GetValue(Data?.Place);

        public override string ToString() => $"{Name} ({Date}, {Place})";

        public Lazy<List<Person?>> Persons {
            get {
                List<Person> persons = GetRepo.Instance<IPersonsRepo>()?
                    .GetAll(person => person.EventId)?
                    .Where(person => person.EventId == Id)?
                    .ToList() ?? new List<Person>();
                return new Lazy<List<Person>>(persons);
            }
        }
        public Lazy<List<Organization?>> Organizations {
            get {
                List<Organization> companies = GetRepo.Instance<IOrganizationsRepo>()?
                    .GetAll(company => company.EventId)?
                    .Where(company => company.EventId == Id)?
                    .ToList() ?? new List<Organization>();
                return new Lazy<List<Organization>>(companies);
            }
        }
    }
}
