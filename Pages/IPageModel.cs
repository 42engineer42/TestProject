namespace Nullam.Pages {
    public interface IPageModel {
        public int PageIndex { get; set; }
        public string? CurrentFilter { get; set; }
        public string? CurrentOrder { get; set; }
        public string? SortOrder(string propertyName);
    }
}