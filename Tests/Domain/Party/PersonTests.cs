using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullam.Aids;
using Nullam.Data.Party;
using Nullam.Domain;
using Nullam.Domain.Party;

namespace Nullam.Tests.Domain.Party {
    [TestClass] public class PersonTests : SealedClassTests<Person, BaseEntity<PersonData>> {
        protected override Person CreateObj() => new(GetRandom.Value<PersonData>());
        [TestMethod] public void FirstNameTest() => IsReadOnly(Obj.Data.FirstName);
        [TestMethod] public void LastNameTest() => IsReadOnly(Obj.Data.LastName);
        [TestMethod] public void PersonalCodeTest() => IsReadOnly(Obj.Data.PersonalCode);
        [TestMethod] public void PayingTypeTest() => IsReadOnly(Obj.Data.PayingType);
        [TestMethod] public void EventIdTest() => IsReadOnly(Obj.Data.EventId);
        [TestMethod] public void EventTest()
            => TestItem<IEventsRepo, Event, EventData>(Obj.EventId, d => new Event(d), () => Obj.Event);

    }
}
