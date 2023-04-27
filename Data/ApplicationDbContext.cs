using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PojisteniApp.Models;

namespace PojisteniApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<PojisteniApp.Models.InsuracePersonData> InsuracePersonData { get; set; } = default!;
        public DbSet<PojisteniApp.Models.PersonInsurance> PersonInsurance { get; set; } = default!;
        public DbSet<PojisteniApp.Models.Insurance> Insurance { get; set; } = default!;
        public DbSet<PojisteniApp.Models.InsuranceInfo> InsuranceInfo { get; set; } = default!;
        public DbSet<PojisteniApp.Models.InsuranceEvent> InsuranceEvent { get; set; } = default!;
    }
}