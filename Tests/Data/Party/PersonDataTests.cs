using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullam.Data;
using Nullam.Data.Party;
using Nullam.Tests;

namespace Nullam.Tests.Data.Party {
    [TestClass] public class PersonDataTests : SealedClassTests<PersonData, BaseData> {
        [TestMethod] public void FirstNameTest() => IsProperty<string?>();

        [TestMethod] public void LastNameTest() => IsProperty<string?>();

        [TestMethod]  public void PersonalCodeTest() => IsProperty<string?>();

        [TestMethod] public void PayingTypeTest() => IsProperty<PayingType?>();

        [TestMethod] public void EventIdTest() => IsProperty<string?>();
    }
}