using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullam.Data.Party;
using Nullam.Domain.Party;

namespace Nullam.Tests.Nullam {
    [TestClass] public class DeletePagesTests : PagesTests {
        [TestMethod] public async Task GetPersonsDeletePageTest()
            => await GetPageTestAsync<IPersonsRepo, Person, PersonData>(x => new Person(x), true);
        [TestMethod] public async Task GetEventsDeletePageTest()
            => await GetPageTestAsync<IEventsRepo, Event, EventData>(x => new Event(x));
        [TestMethod] public async Task GetOrganizationsDeletePageTest()
            => await GetPageTestAsync<IOrganizationsRepo, Organization, OrganizationData>(x => new Organization(x));
    }
}