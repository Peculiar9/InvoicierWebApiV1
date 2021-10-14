using Microsoft.EntityFrameworkCore;

namespace InvoicierWebApiV1.Data.AuthModels
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

    }
}
