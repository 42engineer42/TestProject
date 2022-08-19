using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Nullam.Pages.Extensions {
    public static class EditorHtml {
        public static IHtmlContent MyEditorFor<TModel, TResult>(
            this IHtmlHelper<TModel> h, Expression<Func<TModel, TResult>> e) {
            List<object> s = HtmlStrings(h, e);
            return new HtmlContentBuilder(s);
        }
        private static List<object> HtmlStrings<TModel, TResult>(IHtmlHelper<TModel> h, Expression<Func<TModel, TResult>> e) {
            List<object> l = new() {
                new HtmlString("<dl class=\"row\">"),
                new HtmlString("<dd class=\"col-sm-2\">"),
                h.LabelFor(e, null, new { @class = "control-label" }),
                new HtmlString("</dd>"),
                new HtmlString("<dd class=\"col-sm-10\">"),
                h.EditorFor(e, new { htmlAttributes = new { @class = "form-control" } }),
                h.ValidationMessageFor(e, null, new { @class = "text-danger" }),
                new HtmlString("</dd>"),
                new HtmlString("</dl>")
            };
            return l;
        }
    }
}
