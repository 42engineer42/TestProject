using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Nullam.Tests {
    public abstract class AssertTests {
        protected static void IsTrue(bool? b, string? message = null) => Assert.IsTrue(b ?? false, message ?? String.Empty);
        protected static void IsFalse(bool? b, string? message = null) => Assert.IsFalse(b ?? true, message ?? String.Empty);
        protected static void IsInconclusive(string? str = null) => Assert.Inconclusive(str ?? string.Empty);
        protected static void IsNotNull([NotNull] object? obj = null, string? message = null) => Assert.IsNotNull(obj, message);
        protected static void IsNull(object? obj = null, string? message = null) => Assert.IsNull(obj, message);
        protected static void AreEqual(object? expected, object? actual, string? message = null) => Assert.AreEqual(expected, actual, message);
        protected static void AreNotEqual(object? expected, object? actual, string? message = null) => Assert.AreNotEqual(expected, actual, message);
        protected static void IsInstanceOfType(object obj, Type expectedType, string? message = null) => Assert.IsInstanceOfType(obj, expectedType, message);
        protected static void ArePropertiesEqual(object? objA, object? objB, params string[] exclude) {
            IsNotNull(objA);
            IsNotNull(objB);
            Type? typeA = objA.GetType();
            Type? typeB = objB.GetType();
            foreach (PropertyInfo propertyInfoA in typeA?.GetProperties() ?? Array.Empty<PropertyInfo>()) {
                if (exclude?.Contains(propertyInfoA.Name) ?? false) continue;
                object? valueA = propertyInfoA.GetValue(objA, null);
                PropertyInfo? propertyInfoB = typeB?.GetProperty(propertyInfoA.Name);
                object? valueB = propertyInfoB?.GetValue(objB, null);
                AreEqual(valueA, valueB, $"for property {propertyInfoA.Name}.");
            }
        }
    }
}