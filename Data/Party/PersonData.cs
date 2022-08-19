namespace Nullam.Data.Party {
    public sealed class PersonData : BaseData {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PersonalCode { get; set; }
        public PayingType? PayingType { get; set; }
        public string EventId { get; set; } = string.Empty;
    }
}