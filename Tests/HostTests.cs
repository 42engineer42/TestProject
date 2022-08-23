using System.Diagnostics;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullam.Aids;
using Nullam.Data;
using Nullam.Domain;
using Nullam.Domain.Party;
using Nullam.Infra.Party;
using Nullam.Infra;

namespace Nullam.Tests {
    public abstract class HostTests : AssertTests {
        internal static readonly TestHost<Program> Host;
        internal static readonly HttpClient Client;
        [TestInitialize]
        public virtual void TestInitialize() {
            (GetRepo.Instance<IEventsRepo>() as EventsRepo)?.Clear();
            (GetRepo.Instance<IOrganizationsRepo>() as OrganizationsRepo)?.Clear();
            (GetRepo.Instance<IPersonsRepo>() as PersonsRepo)?.Clear();
        }
        static HostTests() {
            Host = new TestHost<Program>();
            Client = Host.CreateClient();
        }
        protected virtual object? IsReadOnly<T>(string? callingMethod = null) => null;

        protected void TestList<TRepo, TObj, TData>(Action<TData> setId, Func<TData, TObj> toObj, Func<List<TObj>> getList)
            where TRepo : class, IRepo<TObj> where TObj : BaseEntity<TData> where TData : BaseData, new() {

            object? obj = IsReadOnly<List<TObj>>(nameof(TestList));

            IsNotNull(obj);
            if (obj.GetType().Name.Contains("Lazy")) IsInstanceOfType(obj, typeof(Lazy<List<TObj>>));
            else IsInstanceOfType(obj, typeof(List<TObj>));

            TRepo? repo = GetRepo.Instance<TRepo>();
            IsNotNull(repo);

            List<TData> dataList = new();
            int count = GetRandom.Int32(5, 30);

            for (int i = 0; i < count; i++) {
                dynamic? data = GetRandom.Value<TData>();
                if (GetRandom.Bool()) {
                    setId(data);
                    dataList.Add(data);
                }
                repo.Add(toObj(data));
            }

            repo.PageSize = 30;
            AreEqual(count, repo.Get().Count);

            List<TObj> objList = getList();
            AreEqual(dataList.Count, objList.Count);

            foreach (TData data in dataList) {
                TObj? entity = objList.Find(z => z.Id == data.Id);
                IsNotNull(entity);
                ArePropertiesEqual(data, entity, nameof(BaseData.Token));
            }
        }
        protected void TestItem<TRepo, TObj, TData>(string id, Func<TData, TObj> toObj, Func<TObj?> getObj)
            where TRepo : class, IRepo<TObj> where TObj : BaseEntity {

            object? obj = IsReadOnly<TObj>(nameof(TestItem));
            IsNotNull(obj);
            IsInstanceOfType(obj, typeof(TObj));

            TRepo? repo = GetRepo.Instance<TRepo>();
            TData? items = AddRandomItems(out int count, toObj, id, repo);

            repo.PageSize = 30;
            AreEqual(count, repo.Get().Count);
            ArePropertiesEqual(items, getObj(), nameof(BaseData.Token));
        }
        protected static void TestRelatedLists<TRepo, TRelatedItem, TItem, TData>
            (Action relatedTest,
                Func<List<TRelatedItem>> relatedItems,
                Func<List<TItem?>> items,
                Func<TRelatedItem, string> detailId,
                Func<TData, TItem> toObj,
                Func<TItem?, TData?> toData,
                Func<TRelatedItem?, TData?> relatedToData)
            where TRepo : class, IRepo<TItem>
            where TItem : BaseEntity
            where TRelatedItem : BaseEntity {

            relatedTest();
            List<TRelatedItem> list = relatedItems();
            TRepo? repo = GetRepo.Instance<TRepo>();
            foreach (TRelatedItem relatedItem in list) {
                dynamic? data = GetRandom.Value<TData>();
                if (data is not null) data.Id = detailId(relatedItem);
                repo?.Add(toObj(data));
            }
            List<TItem?> characters = items();
            AreEqual(list.Count, characters.Count);
            foreach (TRelatedItem relatedItem in list) {
                TItem? item = characters.Find(x => x?.Id == detailId(relatedItem));
                ArePropertiesEqual(toData(item), relatedToData(relatedItem), nameof(BaseData.Token));
            }
        }
        internal static TData? AddRandomItems<TRepo, TObj, TData>(out int cnt, Func<TData, TObj> toObj, string? id = null, TRepo? repo = null)
            where TRepo : class, IRepo<TObj>
            where TObj : BaseEntity {
            repo ??= GetRepo.Instance<TRepo>();
            dynamic? data = GetRandom.Value<TData>();
            if (id is not null && data is not null) data.Id = id;
            cnt = GetRandom.Int32(5, 30);
            int idx = GetRandom.Int32(0, cnt);
            for (int i = 0; i < cnt; i++) {
                dynamic? x = (i == idx) ? data : GetRandom.Value<TData>();
                IsNotNull(x);
                repo?.Add(toObj(x));
            }
            return data;
        }
        protected static string GetCallingMember(string memberName) {
            StackTrace stackTrace = new();
            bool isNext = false;
            for (int i = 0; i < stackTrace.FrameCount - 1; i++) {
                string name = stackTrace.GetFrame(i)?.GetMethod()?.Name ?? string.Empty;
                if (name is "MoveNext" or "Start") continue;
                if (isNext) return name;
                if (name == memberName) isNext = true;
            }
            return string.Empty;
        }
    }
}