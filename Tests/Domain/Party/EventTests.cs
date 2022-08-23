using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullam.Aids;
using Nullam.Data.Party;
using Nullam.Domain;
using Nullam.Domain.Party;

namespace Nullam.Tests.Domain.Party {
    [TestClass]  public class EventTests : SealedClassTests<Event, BaseEntity<EventData>> {
        protected override Event CreateObj() => new(GetRandom.Value<EventData>());
        [TestMethod] public void NameTest() => IsReadOnly(Obj.Data.Name);
        [TestMethod] public void DateTest() => IsReadOnly(Obj.Data.Date);
        [TestMethod] public void PlaceTest() => IsReadOnly(Obj.Data.Place);
        [TestMethod] public void PersonsTest() => TestList<IPersonsRepo, Person, PersonData>(
            d => d.EventId = Obj.Id, d => new Person(d), () => Obj.Persons.Value);
        [TestMethod] public void OrganizationsTest() => TestList<IOrganizationsRepo, Organization, OrganizationData>(
            d => d.EventId = Obj.Id, d => new Organization(d), () => Obj.Organizations.Value);
        [TestMethod] public void ToStringTest() {
            string expected = $"{Obj.Name} ({Obj.Date}, {Obj.Place})";
            AreEqual(expected, Obj.ToString());
        }
    }
}