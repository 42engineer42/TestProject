using System.Net;
using Nullam.Domain;

namespace Nullam.Tests.Nullam {
    public abstract class PagesTests : HostTests {
        public string? Name;
        public string? Handler;
        public string? Html;
        public HttpResponseMessage? Page;
        //ToDo: needs to be changed according to new layout
        public async Task GetPageTestAsync<TRepo, TObj, TData>(Func<TData, TObj> toObj, bool? authorized = false)
            where TRepo : class, IRepo<TObj>
            where TObj : BaseEntity, new() {

            await SetUpHtmlForTesting<TRepo, TObj, TData>(toObj);

            var testString = GetCorrectTestString(authorized);
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
        public string GetCorrectTestString(bool? authorized)
            => (authorized is false) ? $"<h1>Index</h1>:<h4>{Name}</h4>" : "Identity/Account/Register:Identity/Account/Login";
        public void ControlHtmlString(string s) {
            var strings = s.Split(":") ?? new string[] { s };
            foreach (var line in strings) IsTrue(Html.Contains(line));
        }
    } 
}