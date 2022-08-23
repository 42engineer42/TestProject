using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullam.Data;
using Nullam.Data.Party;

namespace Nullam.Tests.Data.Party {
    [TestClass] public class OrganizationDataTests : SealedClassTests<OrganizationData, BaseData> {
        [TestMethod] public void NameTest() => IsProperty<string?>();

        [TestMethod] public void OrganizationCodeTest() => IsProperty<string?>();

        [TestMethod] public void ParticipantsAmountTest() => IsProperty<int?>();

        [TestMethod] public void PayingTypeTest() => IsProperty<PayingType?>();

        [TestMethod] public void EventIdTest() => IsProperty<string?>();
    }
}