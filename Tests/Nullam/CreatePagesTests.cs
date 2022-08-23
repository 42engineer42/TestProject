using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullam.Data.Party;
using Nullam.Domain.Party;

namespace Nullam.Tests.Nullam {
    [TestClass] public class CreatePagesTests : PagesTests {
        [TestMethod] public async Task GetPersonsCreatePageTest()
            => await GetPageTestAsync<IPersonsRepo, Person, PersonData>(x => new Person(x), true);
        [TestMethod] public async Task GetDepartmentsCreatePageTest()
            => await GetPageTestAsync<IEventsRepo, Event, EventData>(x => new Event(x));
        [TestMethod] public async Task GetTrainersCreatePageTest()
            => await GetPageTestAsync<IOrganizationsRepo, Organization, OrganizationData>(x => new Organization(x));
    }
}