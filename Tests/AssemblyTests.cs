using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullam.Aids;

namespace Nullam.Tests {
    public abstract class AssemblyTests : AssertTests {
        private Assembly? testingAssembly;
        private Assembly? assemblyToBeTested;
        private List<Type>? testingTypes;
        private List<Type>? typesToBeTested;
        private string? namespaceOfTest;
        private string? namespaceOfType;
        private static string TestsStr => "Tests";
        private static string TestsProjectStr => $"{TestsStr}.";
        private string NotTestedMsg => $"Class \"{FullNameOfFirstNotTested()}\" is not tested";
        [TestMethod] public void IsAllTested() => isAllTested();
        protected virtual void isAllTested() {
            testingAssembly = GetTheAssembly(this);
            testingTypes = GetTypes(testingAssembly);
            namespaceOfTest = GetTheNamespace(this);
            RemoveNotInNamespace(testingTypes, namespaceOfTest);
            RemoveNotClassTests();
            RemoveNotCorrectTests();
            namespaceOfType = RemoveTestsTagFrom(namespaceOfTest);
            assemblyToBeTested = GetTheAssembly(namespaceOfType);
            typesToBeTested = GetTypes(assemblyToBeTested);
            RemoveNotInNamespace(typesToBeTested, namespaceOfType);
            RemoveInterface();
            RemoveNotNeedTesting();
            RemoveDuplications();
            RemoveTested();
            if (AllAreTested()) return;
            ReportNotAllIsTested();
        }

        private void RemoveDuplications() => typesToBeTested?.Find(x => IsItDuplicated(x));

        private bool IsItDuplicated(Type givenType) {
            Type? type = typesToBeTested?.Find(y => IsDuplicated(y, givenType));
            if (type == null) return false;
            _ = typesToBeTested?.Remove(type);
            return type is not null;
        }
        private static bool IsDuplicated(Type typeA, Type typeB) {
            if (typeA == typeB) return false;
            string nameX = typeA.Name;
            string nameY = typeB.Name;
            int lengthX = nameX.IndexOf('`');
            int lengthY = nameY.IndexOf('`');
            if (lengthX >= 0) nameX = nameX[..lengthX];
            if (lengthY >= 0) nameY = nameY[..lengthY];
            return nameX == nameY;
        }
        private void RemoveInterface() => typesToBeTested?.RemoveAll(t => t.IsInterface);
        private void RemoveNotClassTests() => testingTypes.Remove(x => !Types.NameEnds(x, TestsStr));
        private void RemoveNotCorrectTests() => testingTypes.Remove(x => !IsCorrectTest(x));
        private static void RemoveNotInNamespace(List<Type>? type, string? nameSpace) => type?.Remove(x => !Types.NameStarts(x, nameSpace));
        private static Assembly? GetTheAssembly(object obj) => GetAssembly.OfType(obj);
        private static Assembly? GetTheAssembly(string? name) => GetAssembly.GetAssemblyByName(name);
        private static string? RemoveTestsTagFrom(string? str) => str?.Remove(TestsProjectStr);
        private static string? GetTheNamespace(object obj) => GetNamespace.OfType(obj);
        private static List<Type>? GetTypes(Assembly? assembly) => GetAssembly.GetTypes(assembly);
        private void ReportNotAllIsTested() => IsInconclusive(NotTestedMsg);
        private string FullNameOfFirstNotTested() => FirstNotTestedType(typesToBeTested)?.FullName ?? string.Empty;
        private static Type? FirstNotTestedType(List<Type>? list) => list.GetFirst();
        private bool AllAreTested() => typesToBeTested.IsEmpty();
        private void RemoveTested() => typesToBeTested?.RemoveAll(x => IsItTested(x));
        private bool IsItTested(Type giventType) {
            Type? type = testingTypes?.Find(y => IsTestFor(y, giventType));
            if (type is null) return false;
            _ = testingTypes?.Remove(type);
            return type is not null;
        }
        private static bool IsCorrectTest(Type type) => IsCorrectlyInherited(type) && IsTestClass(type);
        private static bool IsTestClass(Type type) => type?.HasAttribute<TestClassAttribute>() ?? false;
        private static bool IsCorrectlyInherited(Type type) => type.IsInherited(typeof(TypeTests));
        private static bool IsTestFor(Type testingType, Type typeToBeTested) {
            string testName = typeToBeTested.FullName ?? string.Empty;
            testName = testName.RemoveHead();
            int length = testName.IndexOf('`');
            if (length >= 0) testName = testName[..length];
            testName += TestsStr;
            return testingType.NameEnds($".{testName}");
        }
        private void RemoveNotNeedTesting() => typesToBeTested?.Remove(type => !IsTypeToBeTested(type));
        private bool IsTypeToBeTested(Type type) => type?.BelongsTo(namespaceOfType) ?? false;
    }
}