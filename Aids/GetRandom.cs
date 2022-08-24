
using System.Reflection;

namespace Nullam.Aids {
    public class GetRandom {
        private static void PutMinFirst<T>(ref T min, ref T max) where T : IComparable<T> {
            if (min.CompareTo(max) < 0) return;
            (max, min) = (min, max);
        }
        public static int Int32(int? min = null, int? max = null) {
            int minValue = min ?? -1000;
            int maxValue = max ?? 1000;
            PutMinFirst(ref minValue, ref maxValue);
            return Random.Shared.Next(minValue, maxValue);
        }
        public static long Int64(long? min = null, long? max = null) {
            long minValue = min ?? -1000L;
            long maxValue = max ?? 1000L;
            PutMinFirst(ref minValue, ref maxValue);
            return Random.Shared.NextInt64(minValue, maxValue);
        }
        public static double Double(double? min = null, double? max = null) {
            double minValue = min ?? -1000.0;
            double maxValue = max ?? 1000.0;
            PutMinFirst(ref minValue, ref maxValue); 
            return minValue + (Random.Shared.NextDouble() * (maxValue - minValue));
        }
        public static char Char(char min = char.MinValue, char max = char.MaxValue) => (char)Int32(min, max);
        public static bool Bool() => Int32() % 2 == 0;
        public static DateTime DateTime(DateTime? min = null, DateTime? max = null) {
            long minValue = (min ?? System.DateTime.Now.AddYears(-50)).Ticks;
            long maxValue = (max ?? System.DateTime.Now.AddYears(50)).Ticks;
            PutMinFirst(ref minValue, ref maxValue);
            long v = Int64(minValue,maxValue);
            return System.DateTime.MinValue.AddTicks(v);
        }
        public static string String(ushort minLength = 5, ushort maxLength = 30) {
            string s = string.Empty;
            int length = Int32(minLength, maxLength);
            for (int i = 0; i < length; i++) s += Char('a', 'z');
            return s;
        }
        public static dynamic? Value<T>(T? min = default, T? max = default) {
            Type t = GetUnderlyingType(typeof(T));
            if (IsEnum(t)) return EnumOf(t);
            else if (t == typeof(byte[])) return ConcurrencyToken.ToByteArray(String(1, 8));
            else if (t == typeof(bool)) return Bool();
            else if (t == typeof(DateTime)) return DateTime(Convert.ToDateTime(min), Convert.ToDateTime(max));
            else if (t == typeof(double)) return Double(Convert.ToDouble(min), Convert.ToDouble(max));
            else if (t == typeof(char)) return Char(Convert.ToChar(min), Convert.ToChar(max));
            else if (t == typeof(int)) return Int32(Convert.ToInt32(min), Convert.ToInt32(max));
            else if (t == typeof(long)) return Int64(Convert.ToInt64(min), Convert.ToInt64(max));
            else if (t == typeof(string)) return String();
            return TryGetObject<T>();
        }
        public static dynamic? EnumOf(Type t) {
            if (!t.IsEnum) return null;
            Array values = Enum.GetValues(t);
            int max = values.Length - 1;
            int i = Int32(0, max);
            return values.GetValue(i);
        }
        internal protected static bool IsEnum(Type type) => type.IsEnum;
        public static dynamic? Value(Type t) {
            t = GetUnderlyingType(t);
            if (IsEnum(t)) return EnumOf(t);
            else if (t == typeof(byte[])) return ConcurrencyToken.ToByteArray(String(1, 8));
            else if (t == typeof(bool)) return Bool();
            else if (t == typeof(DateTime)) return DateTime();
            else if (t == typeof(double)) return Double();
            else if (t == typeof(char)) return Char();
            else if (t == typeof(int)) return Int32();
            else if (t == typeof(long)) return Int64();
            else if (t == typeof(string)) return String();
            return null;
        } 
        internal static Type GetUnderlyingType(Type t) {
            Type? x = Nullable.GetUnderlyingType(t);
            return (x is not null) ? x : t;
        } 
        private static T? TryGetObject<T>() {
            T? o = TryCreate<T>();
            foreach (PropertyInfo propertyInfo in o?.GetType()?.GetProperties() ?? Array.Empty<PropertyInfo>()) {
                if(!propertyInfo.CanWrite) continue;
                dynamic? v = Value(propertyInfo.PropertyType);
                propertyInfo.SetValue(o, v, null);
            }
            return o;
        }
        private static T? TryCreate<T>() =>
            Safe.Run(() => {
                ConstructorInfo? c = typeof(T).GetConstructor(Array.Empty<Type>());
                return (c?.Invoke(null) is T t) ? t : default;
            });
    }
}
