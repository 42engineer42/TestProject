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
    [TestClass] public class OrganizationTests : SealedClassTests<Organization, BaseEntity<OrganizationData>> {
        protected override Organization CreateObj() => new(GetRandom.Value<OrganizationData>());
        [TestMethod] public void NameTest() => IsReadOnly(Obj.Data.Name);
        [TestMethod] public void ParticipantsAmountTest() => IsReadOnly(Obj.Data.ParticipantsAmount);
        [TestMethod] public void OrganizationCodeTest() => IsReadOnly(Obj.Data.OrganizationCode);
        [TestMethod] public void PayingTypeTest() => IsReadOnly(Obj.Data.PayingType);
        [TestMethod] public void EventIdTest() => IsReadOnly(Obj.Data.EventId);
        [TestMethod] public void EventTest()
            => TestItem<IEventsRepo, Event, EventData>(Obj.EventId, d => new Event(d), () => Obj.Event);

    }
}
