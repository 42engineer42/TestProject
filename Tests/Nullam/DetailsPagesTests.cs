using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullam.Data.Party;
using Nullam.Domain.Party;

namespace Nullam.Tests.Nullam {
    [TestClass] public class DetailsPagesTests : PagesTests {
        [TestMethod] public async Task GetEventsDetailsPageTest()
            => await GetPageTestAsync<IEventsRepo, Event, EventData>(x => new Event(x), "<h1>Osavõtjad</h1>");
    }
}