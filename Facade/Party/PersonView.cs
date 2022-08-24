
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Nullam.Data;
using Nullam.Data.Party;
using Nullam.Domain.Party;

namespace Nullam.Facade.Party {
    public sealed class PersonView : BaseView {
        [DisplayName("Eesnimi"), Required] public string? FirstName { get; set; }
        [DisplayName("Perenimi"), Required] public string? LastName { get; set; }
        [DisplayName("Isikukood"), Required, PersonalCodeValidation] public string? PersonalCode { get; set; }
        [DisplayName("Maksmisviis"), Required] public PayingType? PayingType { get; set; }
        [DisplayName("Üritus"), Required] public string? EventId { get; set; }
    }
    public sealed class PersonViewFactory : BaseViewFactory<PersonView, Person, PersonData> {
        protected override Person ToEntity(PersonData d) => new(d);
        public override Person Create(PersonView? v) {
            v ??= new PersonView();
            v.PayingType ??= PayingType.Cash;
            return base.Create(v);
        }
        public override PersonView Create(Person? e) {
            var v = base.Create(e);
            v.PayingType = e?.PayingType;
            return v;
        }
    }
}
