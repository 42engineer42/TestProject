namespace Nullam.Aids {
    public static class ConcurrencyToken {
        public static string ToStr(byte[]? token = null) {
            string s = string.Empty;
            foreach (byte b in token ?? Array.Empty<byte>()) s += b;
            return s;
        }
        public static byte[] ToByteArray(string? token = null) {
            List<byte> l = new();
            foreach (char c in token ?? GetRandom.String(8, 8)) l.Add(Convert.ToByte(c));
            return l.ToArray();
        }
    }
}
