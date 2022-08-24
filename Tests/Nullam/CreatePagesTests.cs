using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullam.Data.Party;
using Nullam.Domain.Party;

namespace Nullam.Tests.Nullam {
    [TestClass] public class CreatePagesTests : PagesTests {
        [TestMethod] public async Task GetPersonsCreatePageTest()
            => await GetPageTestAsync<IPersonsRepo, Person, PersonData>(x => new Person(x), "<h1>Osavõtjate lisamine</h1>");
        [TestMethod] public async Task GetEventsCreatePageTest()
            => await GetPageTestAsync<IEventsRepo, Event, EventData>(x => new Event(x), "<h1>Ürituse lisamine</h1>");
        [TestMethod] public async Task GetOrganizationsCreatePageTest()
            => await GetPageTestAsync<IOrganizationsRepo, Organization, OrganizationData>(x => new Organization(x), "<h1>Osavõtjate lisamine</h1>");
    }
}