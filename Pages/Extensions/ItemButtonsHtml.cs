using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Nullam.Pages.Extensions {
    public static class ItemButtonsHtml {
        public static IHtmlContent ItemButtons<TModel>(
            this IHtmlHelper<TModel> h, string id, Dictionary<string, string> pairs, string? pageName) {
            List<object> s = HtmlStrings(h, id, pairs, pageName);
            return new HtmlContentBuilder(s);
        }
        private static List<object> HtmlStrings<TModel>(IHtmlHelper<TModel> h, string id, Dictionary<string, string> pairs, string? pageName) {
            List<object> l = new();
            foreach (var item in pairs) {
                l.Add(h.MyBtn(item.Key, item.Value, id, pageName));
                l.Add(new HtmlString("&nbsp;"));
            }
            return l;
        }
    }
}