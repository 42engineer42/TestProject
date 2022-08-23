
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Nullam.Data;
using Nullam.Data.Party;
using Nullam.Domain.Party;

namespace Nullam.Facade.Party {
    public sealed class OrganizationView : BaseView {
        [DisplayName("Juriidiline nimi"), Required] public string? Name { get; set; }
        [DisplayName("Osavõtjate arv"), Required] public int? ParticipantsAmount { get; set; }
        [DisplayName("Registrikood"), Required] public string? OrganizationCode { get; set; }
        [DisplayName("Maksmisviis"), Required] public PayingType? PayingType { get; set; }
        [DisplayName("Üritus"), Required] public string? EventId { get; set; }
    }
    public sealed class OrganizationViewFactory : BaseViewFactory<OrganizationView, Organization, OrganizationData> {
        protected override Organization ToEntity(OrganizationData d) => new(d);
        public override Organization Create(OrganizationView? v) {
            v ??= new OrganizationView();
            v.PayingType ??= PayingType.Cash;
            return base.Create(v);
        }
        public override OrganizationView Create(Organization? e) {
            var v = base.Create(e);
            v.PayingType = e?.PayingType;
            return v;
        }
    }
}
