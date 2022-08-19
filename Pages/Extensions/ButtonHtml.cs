using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Nullam.Pages.Extensions {
    public static class ButtonHtml {
        public static IHtmlContent MyBtn<TModel>(
            this IHtmlHelper<TModel> h, string title, string handler, string id, string? pageName) {
            List<object> s = HtmlStrings(title, handler, id, h.ViewData.Model as IPageModel, pageName);
            return new HtmlContentBuilder(s);
        }
        private static List<object> HtmlStrings(string title, string handler, string id, IPageModel? m, string? pageName) {
            var name = pageName ?? PageName(m);
            List<object> l = new() {
                new HtmlString($"<a style=\"text-decoration:none;color:Gray;\" "),
                new HtmlString($"href=\"/{name}/{handler}?"),
                new HtmlString($"handler={handler}&amp;"),
                new HtmlString($"id={id}&amp;"),
                new HtmlString($"order={m?.CurrentOrder}&amp;"),
                new HtmlString($"idx={m?.PageIndex ?? 0}&amp;"),
                new HtmlString($"filter={m?.CurrentFilter}\">"),
                new HtmlString($"{title}")
            };
            return l;
        }
        private static string? PageName(IPageModel? m) => m?.GetType()?.Name?.Replace("Page", "");
    }
}