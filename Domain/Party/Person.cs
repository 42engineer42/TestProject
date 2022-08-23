
using System.ComponentModel.DataAnnotations.Schema;
using Nullam.Data;
using Nullam.Data.Party;

namespace Nullam.Domain.Party {
    public interface IPersonsRepo: IRepo<Person> {}
    public sealed class Person: BaseEntity<PersonData> {
        public Person(): this(new()) {}
        public Person(PersonData data): base(data) {}

        public string FirstName => GetValue(Data?.FirstName);
        public string LastName => GetValue(Data?.LastName);
        public string PersonalCode => GetValue(Data?.PersonalCode);
        public PayingType PayingType => GetValue(Data?.PayingType);
        public string EventId => GetValue(Data?.EventId);
        public Event? Event => GetRepo.Instance<IEventsRepo>()?.Get(EventId);
    }
}
