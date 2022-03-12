using InvoicierWebApiV1.Data.EntityModels;
using InvoicierWebApiV1.Service.OrganizationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoicierWebApiV1.Services
{
    public class OrganizationService : IOrganizationServices
    {
        private readonly InvoicierDbContext _dbcontext;

        public OrganizationService(InvoicierDbContext dbContext)
        {
            _dbcontext = dbContext;
        }
        public async Task<IEnumerable<Organization>> GetOrganizations()
        {
            return _dbcontext.Organizations.ToList();
        }

        public async Task<Organization> GetOrganizationById(int id)
        {
            return _dbcontext.Organizations.FirstOrDefault(p => p.OrganizationId == id);
        }

        public async Task CreateOrganization(Organization Organization)
        {
            if (Organization != null)
            {
                try
                {
                    await _dbcontext.AddAsync(Organization);
                }
                catch (Exception ex)
                {
                    throw new ArgumentNullException(ex.ToString(), nameof(Organization));
                }
            }

        }

        public async Task DeleteOrganization(Organization Organization)
        {
            if (Organization != null)
            {
                try
                {
                    _dbcontext.Remove(Organization);
                }
                catch (Exception ex)
                {
                    throw new ArgumentNullException(ex.ToString(), nameof(Organization));
                }
            }
        }

        public Task UpdateOrganization(Organization Organization)
        {
            throw new NotImplementedException();
        }

        public bool SaveChanges()
        {
            return (_dbcontext.SaveChanges() >= 0);
        }

    }
}
