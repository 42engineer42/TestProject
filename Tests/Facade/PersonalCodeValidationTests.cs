using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullam.Aids;
using Nullam.Facade;

namespace Nullam.Tests.Facade {
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClass] public class PersonalCodeValidationTests 
        : BaseTests<PersonalCodeValidation, ValidationAttribute> {
        private class TestClass : PersonalCodeValidation { }
        protected override PersonalCodeValidation CreateObj() => new TestClass();

        [DataRow("60102124920", true)] 
        [DataRow("64502124920", false)] //gender and year doesn't match
        [DataRow("60113124920", false)] //date is not correct
        [DataRow("00102124920", false)] //gender is incorrect
        [DataRow("60102124925", false)] //control number is wrong
        [TestMethod] public void IsValidTest(object value, bool expected) {
            var attribute = new PersonalCodeValidation();
            bool actual = attribute.IsValid(value);
            AreEqual(expected, actual);
        }
    }
}
