using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullam.Data.Party;
using Nullam.Domain.Party;

namespace Nullam.Tests.Nullam {
    [TestClass] public class IndexPagesTests : PagesTests {
        [TestMethod] public async Task GetPersonsIndexPageTest() 
            => await GetPageTestAsync<IPersonsRepo, Person, PersonData>(x => new Person(x), true);
        [TestMethod] public async Task GetOrganizationsIndexPageTest() 
            => await GetPageTestAsync<IOrganizationsRepo, Organization, OrganizationData>(x => new Organization(x));
        [TestMethod] public async Task GetEventsIndexPageTest() 
            => await GetPageTestAsync<IEventsRepo, Event, EventData>(x => new Event(x));
    }
}