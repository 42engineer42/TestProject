using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullam.Data.Party;
using Nullam.Domain.Party;

namespace Nullam.Tests.Nullam {
    [TestClass] public class EditPagesTests : PagesTests {
        [TestMethod] public async Task GetPersonsEditPageTest()
            => await GetPageTestAsync<IPersonsRepo, Person, PersonData>(x => new Person(x), "<h1>Osavõtja info</h1>");
        [TestMethod] public async Task GetOrganizationsEditPageTest()
            => await GetPageTestAsync<IOrganizationsRepo, Organization, OrganizationData>(x => new Organization(x), "<h1>Osavõtja info</h1>");
        [TestMethod] public async Task GetEventsEditPageTest()
            => await GetPageTestAsync<IEventsRepo, Event, EventData>(x => new Event(x), "<h1>Osavõtja info</h1>");
    }
}