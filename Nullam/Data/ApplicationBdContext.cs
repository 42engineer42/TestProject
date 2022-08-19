using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Nullam.Infra;

namespace Nullam.Data {
    public class ApplicationDbContext : IdentityDbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder b) {
            base.OnModelCreating(b);
            InitializeTables(b);
        }
        private static void InitializeTables(ModelBuilder b) {
            NullamDb.InitializeTables(b);
        }
    }
}
