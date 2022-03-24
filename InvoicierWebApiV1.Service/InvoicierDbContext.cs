using InvoicierWebApiV1.Data.AuthModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using InvoicierWebApiV1.Core.EntityModels;
// using Microsoft.EntityFrameworkCore;

namespace InvoicierWebApiV1.Infrastructure
{
    public class InvoicierDbContext : IdentityDbContext<ApplicationUser>
    {
        public InvoicierDbContext(DbContextOptions<InvoicierDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<OrganizationAddress> OrganizationsAddress { get; set; }
    }
}

      
            