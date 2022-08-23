using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullam.Data.Party;
using Nullam.Domain.Party;

namespace Nullam.Tests.Nullam {
    [TestClass] public class DetailsPagesTests : PagesTests {
        [TestMethod] public async Task GetEventsDetailsPageTest()
            => await GetPageTestAsync<IEventsRepo, Event, EventData>(x => new Event(x), true);
        [TestMethod] public async Task GetPersonsDetailsPageTest()
            => await GetPageTestAsync<IPersonsRepo, Person, PersonData>(x => new Person(x));
        [TestMethod] public async Task GetOrganizationsDetailsPageTest()
            => await GetPageTestAsync<IOrganizationsRepo, Organization, OrganizationData>(x => new Organization(x));
    }
}