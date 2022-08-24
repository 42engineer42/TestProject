using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullam.Facade;

namespace Nullam.Tests.Facade {
    [TestClass] public class PersonalCodeValidationTests : AbstractClassTests<PersonalCodeValidation, ValidationAttribute> {
        protected override PersonalCodeValidation CreateObj() {
            throw new NotImplementedException();
        }
    }
}
