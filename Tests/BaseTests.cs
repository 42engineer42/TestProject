using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullam.Aids;

namespace Nullam.Tests {
    public abstract class BaseTests<TClass, TBaseClass> : TypeTests where TClass : class where TBaseClass : class {
        protected TClass Obj;
        private readonly BindingFlags allFlags = BindingFlags.Public
                                                 | BindingFlags.NonPublic
                                                 | BindingFlags.Instance
                                                 | BindingFlags.Static;
        protected BaseTests() => Obj = CreateObj();
        protected abstract TClass CreateObj();
        protected void IsProperty<T>(T? value = default, bool isReadOnly = false, string? callingMethod = null) {
            callingMethod ??= nameof(IsProperty);
            object? actual = GetProperty(ref value, isReadOnly, callingMethod);
            AreEqual(value, actual);
        }
        protected PropertyInfo? IsDisplayNamed<T>(string? displayName = null, T? value = default, bool isReadOnly = false, string? callingMethod = null) {
            callingMethod ??= nameof(IsDisplayNamed);
            PropertyInfo? propertyInfo = GetPropertyInfo(callingMethod);
            IsProperty(value, isReadOnly, callingMethod);
            if (propertyInfo is null) return propertyInfo;
            DisplayNameAttribute? a = propertyInfo.GetAttribute<DisplayNameAttribute>();
            AreEqual(displayName, a?.DisplayName, nameof(DisplayNameAttribute));
            return propertyInfo;
        }
        protected void IsRequired<T>(string? displayName = null, T? value = default, bool isReadOnly = false) {
            PropertyInfo? propertyInfo = IsDisplayNamed(displayName, value, isReadOnly, nameof(IsRequired));
            IsTrue(propertyInfo?.HasAttribute<RequiredAttribute>(), nameof(RequiredAttribute));
        }
        protected PropertyInfo? GetPropertyInfo(string callingMethod) {
            string memberName = GetCallingMember(callingMethod).Replace("Test", string.Empty);
            return Obj.GetType().GetProperty(memberName, allFlags);

        }
        protected object? GetProperty<T>(ref T? value, bool isReadOnly, string callingMethod) {
            PropertyInfo? propertyInfo = GetPropertyInfo(callingMethod);
            IsNotNull(propertyInfo);
            if (!isReadOnly && IsNullOrDefault(value)) value = Random<T>();
            if (CanWrite(propertyInfo, isReadOnly)) propertyInfo.SetValue(Obj, value);
            return propertyInfo.GetValue(Obj);
        }
        protected override object? IsReadOnly<T>(string? callingMethod = null) {
            T? value = default;
            return GetProperty(ref value, true, callingMethod ?? nameof(IsReadOnly));
        }
        protected void IsReadOnly<T>(T? value) => IsProperty(value, true, nameof(IsReadOnly));
        private static bool IsNullOrDefault<T>(T? value) => value?.Equals(default(T)) ?? true;
        private static bool CanWrite(PropertyInfo propertyInfo, bool isReadOnly) {
            bool canWrite = propertyInfo?.CanWrite ?? false;
            AreEqual(canWrite, !isReadOnly);
            return canWrite;
        }
        private static T? Random<T>() => GetRandom.Value<T>();

        internal protected static void ArePropertiesEqual(object? objectA, object? objectB) {
            PropertyInfo[] propertyInfos = Array.Empty<PropertyInfo>();
            PropertyInfo[] propertyInfosA = objectA?.GetType()?.GetProperties() ?? propertyInfos;
            bool hasProperties = false;
            foreach (PropertyInfo propertyInfoA in propertyInfosA) {
                object? objA = propertyInfoA.GetValue(objectA, null);
                PropertyInfo? propertyInfoB = objectB?.GetType()?.GetProperty(propertyInfoA.Name);
                if (propertyInfoB is null) continue;
                object? objB = propertyInfoB?.GetValue(objectB, null);
                AreEqual(objA, objB);
                hasProperties = true;
            }
            IsTrue(hasProperties, $"No properties found for {objectA}");
        }
        [TestMethod] public void BaseClassTest() => AreEqual(typeof(TClass).BaseType, typeof(TBaseClass));
        protected void IsAbstractMethod(string name, params Type[] args) {
            MethodInfo? methodInfo = typeof(TClass).GetMethod(name, args);
            AreEqual(true, methodInfo?.IsAbstract, name);
        }
    }
}