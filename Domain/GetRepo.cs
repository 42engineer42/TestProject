using Microsoft.Extensions.DependencyInjection;

namespace Nullam.Domain {
    public static class GetRepo {
        internal static IServiceProvider? Service;
        public static TRepo? Instance<TRepo>() where TRepo : class 
            => Service?.CreateScope()?.ServiceProvider.GetRequiredService<TRepo>();
        public static void SetService(IServiceProvider s) => Service = s;
    }
}
