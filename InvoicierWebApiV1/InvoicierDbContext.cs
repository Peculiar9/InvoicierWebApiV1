using InvoicierWebApiV1.Data.EntityModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoicierWebApiV1
{
    public class InvoicierDbContext : DbContext
    {
        public InvoicierDbContext(DbContextOptions<InvoicierDbContext> options) : base(options)
        {

        }

        public DbSet<Organization> Organizations { get; set; }
    }
}
