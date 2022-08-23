using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullam.Infra;

namespace Nullam.Tests.Infra {
    [TestClass] public class NullamDbTests : SealedBaseTests<NullamDb, DbContext> {
        protected override NullamDb CreateObj() => null;
        protected override void isSealedTest() => IsTrue(typeof(NullamDb).IsSealed);
    }
}