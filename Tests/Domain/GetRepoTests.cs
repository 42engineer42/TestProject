using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullam.Domain;
using Nullam.Domain.Party;
using Nullam.Infra.Party;

namespace Nullam.Tests.Domain {
    [TestClass] public abstract class GetRepoTests : global::Nullam.Tests.TypeTests {
        private class TestClass : IServiceProvider {
            public object? GetService(Type serviceType) {
                throw new NotImplementedException();
            }
        }
        [TestMethod] public void InstanceTest() => Assert.IsInstanceOfType(GetRepo.Instance<IEventsRepo>(), typeof(EventsRepo));
        [TestMethod] public void SetServiceTest() {
            IServiceProvider? s = GetRepo.Service;
            TestClass x = new();
            GetRepo.SetService(x);
            AreEqual(x, GetRepo.Service);
            GetRepo.Service = s;
        }
    }
}