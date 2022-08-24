using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullam.Data.Party;
using Nullam.Domain.Party;

namespace Nullam.Tests.Nullam {
    [TestClass] public class IndexPagesTests : PagesTests {
        [TestMethod] public async Task GetEventsIndexPageTest() 
            => await GetPageTestAsync<IEventsRepo, Event, EventData>(x => new Event(x), "<h4>Tulevased üritused</h4>");
    }
}