using Nullam.Facade;

namespace Nullam.Pages {
    public interface IIndexModel<TView> where TView: BaseView{
        public string[] IndexColumns { get; }
        public string[] ColumnsForOrganization { get; }
        public string[] ColumnsForPerson { get; }
        public string TitleForAddingNewObject { get; }
        public string TitleForDetailsView { get; }
        public IList<TView>? Items { get; set; }
        object? GetValue<T>(string name, T v);
        string? GetDisplayName<T>(string propertyName);
    }
}