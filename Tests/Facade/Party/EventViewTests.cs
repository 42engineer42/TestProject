using System.ComponentModel.DataAnnotations;
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

    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClass] public class EventDateValidationTests
        : BaseTests<EventDateValidation, ValidationAttribute> {
        private class TestClass : EventDateValidation { }
        protected override EventDateValidation CreateObj() => new TestClass();

        [DataRow("20.07.2021", false)]
        [DataRow("20.07.2023", true)] 
        [DataRow("20.087.2021", false)] 
        [DataRow("20/07/2024 23:40:00", true)]
        [DataRow("20.07.2024 23:40:00", true)]
        [TestMethod] public void IsValidTest(object value, bool expected) {
            var attribute = new EventDateValidation();
            bool actual = attribute.IsValid(value);
            AreEqual(expected, actual);
        }
    }
}