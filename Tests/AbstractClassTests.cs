using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Nullam.Tests {
    public abstract class AbstractClassTests<TClass, TBaseClass>
        : BaseTests<TClass, TBaseClass>
        where TClass : class where TBaseClass : class {
        [TestMethod] public void IsAbstractTest() => IsTrue(Obj?.GetType()?.BaseType?.IsAbstract ?? false);
    }
}