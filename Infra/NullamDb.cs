using Microsoft.EntityFrameworkCore;
using Nullam.Data.Party;

namespace Nullam.Infra {
    public sealed class NullamDb : DbContext {
        public NullamDb(DbContextOptions<NullamDb> options) : base(options) { }
        public DbSet<EventData>? Events { get; set; }
        public DbSet<PersonData>? Persons { get; set; }
        public DbSet<OrganizationData>? Organizations { get; set; }
        protected override void OnModelCreating(ModelBuilder b) {
            base.OnModelCreating(b);
            InitializeTables(b);
        }
        public static void InitializeTables(ModelBuilder b) {
            string s = nameof(NullamDb)[0..^2];
            _ = (b?.Entity<EventData>()?.ToTable(nameof(Events), s));
            _ = (b?.Entity<PersonData>()?.ToTable(nameof(Persons), s));
            _ = (b?.Entity<OrganizationData>()?.ToTable(nameof(Organizations), s));
        }
    }
}
