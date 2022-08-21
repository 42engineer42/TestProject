using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Nullam.Facade {
    public abstract class BaseView {
        [Required] public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required] public byte[] Token { get; set; } = Array.Empty<byte>();
        [DisplayName("Lisainfo"), MaxLength(1500)] public string? AdditionalInfo { get; set; }
    }
}
