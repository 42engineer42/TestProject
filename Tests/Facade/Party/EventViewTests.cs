using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullam.Data.Party;
using Nullam.Domain.Party;
using Nullam.Facade;
using Nullam.Facade.Party;

namespace Nullam.Tests.Facade.Party {
    [TestClass] public class EventViewTests : SealedClassTests<EventView, BaseView> {
        [TestMethod] public void NameTest() => IsRequired<string?>("Ürituse nimi");
        [TestMethod] public void DateTest() => IsDisplayNamed<DateTime?>("Toimumisaeg");
        [TestMethod] public void PlaceTest() => IsDisplayNamed<string?>("Koht");
    }

    [TestClass] public class EventViewFactoryTests
        : ViewFactoryTests<EventViewFactory, EventView, Event, EventData> {
        protected override Event ToObject(EventData d) => new(d);
        [TestMethod] public override void CreateTest() { }
    }
}