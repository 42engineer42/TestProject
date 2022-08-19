using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Nullam.Pages.Extensions {
    public static class TabHiderHtml {
        public static IHtmlContent MyTabHdr<TModel>(
            this IHtmlHelper<TModel> h, string? name) {
            List<object> s = HtmlStrings(name, h.ViewData.Model as IPageModel);
            return new HtmlContentBuilder(s);
        }
        private static List<object> HtmlStrings(string? name, IPageModel? m) {
            name ??= "Unspecified";
            List<object> l = new() {
                new HtmlString($"<a style=\"text-decoration:none;\" href=\"/{PageName(m)}?"),
                new HtmlString($"handler=Index&amp;"),
                new HtmlString($"order={m?.SortOrder(name)}&amp;"),
                new HtmlString($"idx={m?.PageIndex ?? 0}&amp;"),
                new HtmlString($"filter={m?.CurrentFilter}\">"),
                new HtmlString($"{name}</a>")
            };
            return l;
        }
        private static string? PageName(IPageModel? m) => m?.GetType()?.Name?.Replace("Page", "");
    }
}
