using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullam.Aids;

namespace Nullam.Tests.Aids {
    [TestClass] public class CharsTests : TypeTests {
        private char letter;
        private char digit;
        [TestInitialize] public void Init() {
            letter = GetRandom.Char('a', 'z');
            digit = GetRandom.Char('0', '9');
        }
        [TestMethod] public void IsNameCharTest() {
            Assert.IsTrue(Chars.IsNameChar(letter));
            Assert.IsTrue(Chars.IsNameChar(digit));
            Assert.IsFalse(Chars.IsNameChar('.'));
            Assert.IsTrue(Chars.IsNameChar('`'));
            Assert.IsFalse(Chars.IsNameChar('_'));
            Assert.IsFalse(Chars.IsNameChar(':'));
        }
        [TestMethod] public void IsFullNameCharTest() {
            Assert.IsTrue(Chars.IsFullNameChar(letter));
            Assert.IsTrue(Chars.IsFullNameChar(digit));
            Assert.IsTrue(Chars.IsFullNameChar('.'));
            Assert.IsTrue(Chars.IsFullNameChar('`'));
            Assert.IsFalse(Chars.IsFullNameChar('_'));
            Assert.IsFalse(Chars.IsFullNameChar(':'));
        }
    }
}