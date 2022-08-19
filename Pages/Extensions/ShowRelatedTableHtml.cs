using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nullam.Domain;
using Nullam.Domain.Party;
using Nullam.Facade;
using Nullam.Facade.Party;

namespace Nullam.Pages.Extensions {
    public static class ShowRelatedTableHtml {
        public static IHtmlContent ShowRelatedTable<TModel, TView, TEntity, T>(this IHtmlHelper<TModel> h, IList<TEntity>? items, TView x, T y, string[] columnNames, Dictionary<string, string> pairs)
                where TModel : IIndexModel<TView> 
                where TView : BaseView
                where TEntity : BaseEntity {
            return new HtmlContentBuilder(HtmlStrings(h, items, x, y, columnNames, pairs));
        }
        private static List<object> HtmlStrings<TModel, TView, TEntity, T>(IHtmlHelper<TModel> h, IList<TEntity>? items, TView x, T y, string[] columnNames, Dictionary<string, string> pairs)
            where TModel : IIndexModel<TView> 
            where TView : BaseView
            where TEntity: BaseEntity {
            string pageName = GetPageName(y);
            TModel? m = h.ViewData.Model;
            List<object> l = new() {
                new HtmlString("<table class=\"table\">"),
                new HtmlString("<thead>"),
                new HtmlString("<tr>")
            };
            
            l.Add(new HtmlString("</tr>"));
            l.Add(new HtmlString("</thead>"));
            l.Add(new HtmlString("<tbody>"));
            foreach (TEntity item in items) {
                l.Add(new HtmlString("<tr>"));
                foreach (string name in columnNames) {
                    l.Add(new HtmlString("<td>"));
                    l.Add(h.Raw(m.GetValue<TEntity>(name, item)));
                    l.Add(new HtmlString("</td>"));
                }
                l.Add(new HtmlString("<td>"));
                l.Add(h.ItemButtons(item.Id, pairs, pageName));
                l.Add(new HtmlString("</td>"));
                l.Add(new HtmlString("</tr>"));
            }
            l.Add(new HtmlString("</tbody>"));
            l.Add(new HtmlString("</table>"));
            return l;
        }

        private static string GetPageName<T>(T view) {
            var typeFullName = typeof(T).ToString();
            var typeName = typeFullName.Split(".").Last();
            return typeName.Replace("View", "s");
        }
    }
}