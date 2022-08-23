
using System.ComponentModel.DataAnnotations;

namespace Nullam.Data.Party {
    public sealed class EventData: BaseData {
        [Key]
        public string? Name { get; set; }
        public DateTime? Date { get; set; }
        public string? Place { get; set; }
    }
}
