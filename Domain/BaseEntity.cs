
using System.Runtime.CompilerServices;
using Nullam.Data;

namespace Nullam.Domain {
    public abstract class BaseEntity {
        public static string DefaultStr => "Undefined";
        public static int DefaultInt => 0;
        public static DateTime DefaultDate => DateTime.MinValue;
        protected static string GetValue(string? v) => v ?? DefaultStr;
        protected static int GetValue(int? v) => v ?? DefaultInt;
        protected static PayingType GetValue(PayingType? v) => v ?? PayingType.Cash;
        protected static DateTime GetValue(DateTime? v) => v ?? DefaultDate;
        public abstract string Id { get; }
        public abstract byte[] Token { get; }
    } 
    public abstract class BaseEntity<TData> : BaseEntity where TData : BaseData, new() {
        public TData Data { get; }
        public BaseEntity() : this(new TData()) { }
        public BaseEntity(TData d) => Data = d; 
        public override string Id => GetValue(Data?.Id);
        public string AdditionalInfo => GetValue(Data?.AdditionalInfo);
        public override byte[] Token => Data?.Token ?? Array.Empty<byte>();
    }
}
