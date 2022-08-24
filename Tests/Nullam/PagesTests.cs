using System.Net;
using Nullam.Domain;

namespace Nullam.Tests.Nullam {
    public abstract class PagesTests : HostTests {
        public string? Name;
        public string? Handler;
        public string? Html;
        public HttpResponseMessage? Page;
        //ToDo: needs to be changed according to new layout
        public async Task GetPageTestAsync<TRepo, TObj, TData>(Func<TData, TObj> toObj, string testString)
            where TRepo : class, IRepo<TObj>
            where TObj : BaseEntity, new() {

            await SetUpHtmlForTesting<TRepo, TObj, TData>(toObj);

            ControlHtmlString(testString);
        }
        public async Task SetUpHtmlForTesting<TRepo, TObj, TData>(Func<TData, TObj> toObj)
            where TRepo : class, IRepo<TObj>
            where TObj : BaseEntity, new() {
            
            _ = AddRandomItems<TRepo, TObj, TData>(out int cnt, toObj);
           
            await GetPage<TObj>();
            ControlPageStatues();
            Html = await Page.Content.ReadAsStringAsync();
        }
        public async Task GetPage<TObj>() where TObj : BaseEntity, new() {
            Name = GetName(new TObj()) ?? string.Empty;
            Handler = GetHandler() ?? string.Empty;
            Page = await Client.GetAsync($"/{Name}?handler={Handler}");
        }
        public static string? GetName<TObj>(TObj? obj) {
            var typeName = obj?.GetType().Name;
            return !typeName.EndsWith('y') ? typeName + "s" : typeName[..^1] + "ies";
        }
        public string GetHandler()
            => GetCallingMember(nameof(GetPageTestAsync))
                .Replace("PageTest", string.Empty)
                .Replace("Get", string.Empty)
                .Replace(Name, string.Empty);
        public void ControlPageStatues() => AreEqual(HttpStatusCode.OK, Page.StatusCode);
        public void ControlHtmlString(string line) => IsTrue(Html.Contains(line));
    } 
}