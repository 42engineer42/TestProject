using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullam.Aids;

namespace Nullam.Tests {
    public abstract class TypeTests : HostTests {
        private string? nameOfTest;
        private string? nameOfType;
        private string? namespaceOfTest;
        private string? namespaceOfType;
        private Assembly? assemblyToBeTested;
        private Type? typeToBeTested;
        private List<string>? membersOfType;
        private List<string>? membersOfTest;

        [TestMethod] public void IsAllTested() => isAllTested();
        protected virtual void isAllTested() {
            nameOfTest = GetName(this);
            nameOfType = RemoveTestsTagFrom(nameOfTest);
            namespaceOfTest = GetTheNamespace(this);
            namespaceOfType = RemoveTestsTagFrom(namespaceOfTest);
            assemblyToBeTested = GetTheAssembly(namespaceOfType);
            typeToBeTested = GetType(assemblyToBeTested, nameOfType);
            membersOfType = GetMembers(typeToBeTested);
            membersOfTest = GetMembers(GetType());
            RemoveNotTests(GetType());
            RemoveNotNeedTesting();
            RemoveTested();
            if (AllAreTested()) return;
            ReportNotAllIsTested();
        }

        private void ReportNotAllIsTested() => IsInconclusive($"Member \"{NameOfFirstNotTested()}\" is not tested");
        private string NameOfFirstNotTested() => membersOfType?.GetFirst() ?? string.Empty;
        private bool AllAreTested() => membersOfType.IsEmpty();
        private void RemoveTested() => membersOfType?.Remove(x => IsItTested(x));
        private bool IsItTested(string str) => membersOfTest?.ContainsItem(y => IsTestFor(y, str)) ?? false;
        private static bool IsTestFor(string testingMember, string memberToBeTested)
            => testingMember.Equals(memberToBeTested + "Test");
        private void RemoveNotNeedTesting() => membersOfType?.Remove(x => !IsTypeToBeTested(x));
        private static bool IsTypeToBeTested(string str) => str?.IsTypeName() ?? false;
        private void RemoveNotTests(Type type) => membersOfTest?.Remove(x => !IsCorrectTestMethod(x, type));
        private static bool IsCorrectTestMethod(string str, Type type) => IsCorrectlyInherited(type) && IsTestClass(type) && IsTestMethod(str, type);
        private static bool IsTestClass(Type type) => type?.HasAttribute<TestClassAttribute>() ?? false;
        private static bool IsTestMethod(string methodName, Type type) => type?.GetMethod(methodName).HasAttribute<TestMethodAttribute>() ?? false;
        private static bool IsCorrectlyInherited(Type type) => type.IsInherited(typeof(TypeTests));

        private static List<string>? GetMembers(Type? type) => type?.GetDeclaredMembers();
        private static Type? GetType(Assembly? assembly, string? name) {
            if (string.IsNullOrWhiteSpace(name)) return null;
            foreach (TypeInfo typeInfo in assembly?.DefinedTypes ?? Array.Empty<TypeInfo>())
                if (typeInfo.Name.StartsWith(name)) return typeInfo.AsType();
            return null;
        }
        private static Assembly? GetTheAssembly(string? name) {
            while (!string.IsNullOrWhiteSpace(name)) {
                Assembly? assembly = GetAssembly.GetAssemblyByName(name);
                if (assembly is not null) return assembly;
                name = name.RemoveTail();
            }
            return null;
        }
        private static string? GetTheNamespace(object obj) => GetNamespace.OfType(obj);
        private static string? RemoveTestsTagFrom(string? str) => str?.Remove("Tests")?.Remove("Test")?.Replace("..", ".");
        private static string? GetName(object obj) => Types.GetName(obj?.GetType());
    }
}