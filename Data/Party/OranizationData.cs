namespace Nullam.Data.Party {
    public sealed class OrganizationData : BaseData {
        public string? Name { get; set; }
        public string? OrganizationCode { get; set; }
        public int? ParticipantsAmount { get; set; }
        public PayingType? PayingType { get; set; }
        public string EventId { get; set; } = string.Empty;
    }
}