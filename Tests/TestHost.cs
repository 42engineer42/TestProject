using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Nullam.Data;
using Nullam.Infra;

namespace Nullam.Tests {
    public class TestHost<TProgram> : WebApplicationFactory<TProgram> where TProgram : class {
        protected override void ConfigureWebHost(IWebHostBuilder builder) {
            base.ConfigureWebHost(builder);
            builder.ConfigureTestServices(service => {
                RemoveDb<ApplicationDbContext>(service);
                RemoveDb<NullamDb>(service);
                service.AddEntityFrameworkInMemoryDatabase();
                AddDb<ApplicationDbContext>(service);
                AddDb<NullamDb>(service);
                EnsureCreated(service, typeof(ApplicationDbContext), typeof(NullamDb));
            });
        }
        private static void EnsureCreated(IServiceCollection service, params Type[] types) {
            ServiceProvider? serviceProvider = service.BuildServiceProvider();
            using IServiceScope scope = serviceProvider.CreateScope();
            IServiceProvider scopedServices = scope.ServiceProvider;
            foreach (Type type in types) EnsureCreated(scopedServices, type);
        }
        private static void EnsureCreated(IServiceProvider serviceProvider, Type type) {
            if (serviceProvider?.GetRequiredService(type) is not DbContext c)
                throw new ApplicationException($"DBContext {type} not found");
            c?.Database?.EnsureCreated();
            if (!(c?.Database?.IsInMemory() ?? false))
                throw new ApplicationException($"DBContext {type} is not in memory");
        }
        private static void AddDb<T>(IServiceCollection service) where T : DbContext
            => service?.AddDbContext<T>(o => { o.UseInMemoryDatabase("Tests"); });
        private static void RemoveDb<T>(IServiceCollection service) where T : DbContext {
            ServiceDescriptor? descriptor = service?.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<T>));
            if (descriptor is not null) { service?.Remove(descriptor); }
        }
    }
}