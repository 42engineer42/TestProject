using System.ComponentModel.DataAnnotations;

namespace Nullam.Data {
    public abstract class BaseData {
        public static string NewId => Guid.NewGuid().ToString();
        public string Id { get; set; } = NewId;
        public string? AdditionalInfo { get; set; }
        protected static byte[] Empty => Array.Empty<byte>();
        [Timestamp] public byte[] Token { get; set; } = Empty;
    }
}
