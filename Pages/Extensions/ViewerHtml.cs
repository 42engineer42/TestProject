using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Nullam.Pages.Extensions {
    public static class ViewerHtml {
        public static IHtmlContent MyViewerFor<TModel, TResult>(
            this IHtmlHelper<TModel> h, Expression<Func<TModel, TResult>> e) {
            List<object> s = HtmlStrings(h, e);
            return new HtmlContentBuilder(s);
        }
        public static IHtmlContent MyViewerFor<TModel, TResult>(
            this IHtmlHelper<TModel> h, Expression<Func<TModel, TResult>> e, dynamic? value) {
            dynamic? s = HtmlStrings(h, e, value);
            return new HtmlContentBuilder(s);
        }
        private static List<object> HtmlStrings<TModel, TResult>(IHtmlHelper<TModel> h, Expression<Func<TModel, TResult>> e) {
            List<object> l = new() {
                new HtmlString("<dl class=\"row\">"),
                new HtmlString("<dt class=\"col-sm-2\">"),
                h.DisplayNameFor(e),
                new HtmlString("</dt>"),
                new HtmlString("<dd class=\"col-sm-10\">"),
                h.DisplayFor(e),
                new HtmlString("</dd>"),
                new HtmlString("</dl>")
            };
            return l;
        }
        private static List<object> HtmlStrings<TModel, TResult>(IHtmlHelper<TModel> h
            , Expression<Func<TModel, TResult>> e, dynamic value) {
            List<object> l = new() {
                new HtmlString("<dl class=\"row\">"),
                new HtmlString("<dt class=\"col-sm-2\">"),
                h.DisplayNameFor(e),
                new HtmlString("</dt>"),
                new HtmlString("<dd class=\"col-sm-10\">"),
                h.Raw(value),
                new HtmlString("</dd>"),
                new HtmlString("</dl>")
            };
            return l;
        }
    }
}
