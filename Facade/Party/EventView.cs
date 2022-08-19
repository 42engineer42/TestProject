
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Nullam.Data.Party;
using Nullam.Domain.Party;

namespace Nullam.Facade.Party {
    public sealed class EventView : BaseView {
        [DisplayName("Ürituse nimi"), Required] public string? Name { get; set; }
        [DisplayName("Toimumisaeg"), Required] public DateTime? Date { get; set; }
        [DisplayName("Koht"), Required] public string? Place { get; set; }
        public string? FullInfo { get; set; }
    }
    public sealed class EventViewFactory : BaseViewFactory<EventView, Event, EventData> {
        protected override Event ToEntity(EventData d) => new(d);
        public override EventView Create(Event? e) {
            EventView v = base.Create(e);
            v.FullInfo = e?.ToString();
            return v;
        }
    }
}
