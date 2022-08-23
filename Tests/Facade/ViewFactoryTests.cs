using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullam.Aids;
using Nullam.Data;
using Nullam.Domain;
using Nullam.Facade;

namespace Nullam.Tests.Facade {
    public abstract class ViewFactoryTests<TFactory, TView, TObj, TData>
        : SealedClassTests<TFactory, BaseViewFactory<TView, TObj, TData>>
        where TFactory : BaseViewFactory<TView, TObj, TData>, new()
        where TView : class, new()
        where TData : BaseData, new()
        where TObj : BaseEntity<TData> {
        [TestMethod] public virtual void CreateTest() { }
        [TestMethod]
        public void CreateViewTest() {
            dynamic? view = GetRandom.Value<TView>();
            dynamic? obj = Obj.Create(view);
            ArePropertiesEqual(view, obj.Data);
        }
        [TestMethod]
        public void CreateObjectTest() {
            dynamic? data = GetRandom.Value<TData>();
            dynamic? view = Obj.Create(ToObject(data));
            ArePropertiesEqual(data, view);
        }
        protected abstract TObj ToObject(TData d);
    }
}