using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nullam.Facade;

namespace Nullam.Pages.Extensions {
    public static class ShowTableHtml {
        public static IHtmlContent ShowTable<TModel, TView>(
            this IHtmlHelper<TModel> h, IList<TView>? items, Dictionary<string, string> pairs)
                where TModel : IIndexModel<TView> where TView : BaseView {
            List<object> s = HtmlStrings(h, items, pairs);
            return new HtmlContentBuilder(s);
        }
        private static List<object> HtmlStrings<TModel, TView>(IHtmlHelper<TModel> h, IList<TView>? items, Dictionary<string, string> pairs)
            where TModel : IIndexModel<TView> where TView : BaseView {
            TModel? m = h.ViewData.Model;
            List<object> l = new() {
                new HtmlString("<table class=\"table\">"),
                new HtmlString("<thead>"),
                new HtmlString("<tr>")
            };
            l.Add(new HtmlString("<th></th>"));
            l.Add(new HtmlString("</tr>"));
            l.Add(new HtmlString("</thead>"));
            l.Add(new HtmlString("<tbody>"));
            foreach (TView item in items ?? new List<TView>()) {
                l.Add(new HtmlString("<tr>"));
                foreach (string name in m.IndexColumns) {
                    l.Add(new HtmlString("<td>"));
                    l.Add(h.Raw(m.GetValue(name, item)));
                    l.Add(new HtmlString("</td>"));
                }
                l.Add(new HtmlString("<td>"));
                l.Add(h.ItemButtons(item.Id, pairs, null));
                l.Add(new HtmlString("</td>"));
                l.Add(new HtmlString("</tr>"));
            }
            l.Add(new HtmlString("</tbody>"));
            l.Add(new HtmlString("</table>"));
            return l;
        }
    }
}
