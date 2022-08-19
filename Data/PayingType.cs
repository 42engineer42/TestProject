using System.ComponentModel;

namespace Nullam.Data {
    public enum PayingType {
        [Description("Pangaülekanne")] Transfer = 0,
        [Description("Sularaha")] Cash = 1,
    }
}
