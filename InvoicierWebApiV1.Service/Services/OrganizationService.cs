using InvoicierWebApiV1.Core.EntityModels;
using InvoicierWebApiV1.Core.Interfaces.OrganizationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoicierWebApiV1.Infrastructure.Service
{
    public class OrganizationService : IOrganizationServices
    {
        private readonly InvoicierDbContext _dbcontext;

        public OrganizationService(InvoicierDbContext dbContext)
        {
            _dbcontext = dbContext;
        }
        public async Task<IEnumerable<Organization>> GetOrganizations(string id)
        {
            return _dbcontext.Organizations.Where(d => d.UserId == id);
        }
        //public async Task<IEnumerable<OrganizationAddress>> GetOrganizationAddresses()
        //{
        //    return _dbcontext.OrganizationsAddress.ToList();
        //}
        public async Task<IEnumerable<Organization>> GetAllOrganizations()
        {
            return _dbcontext.Organizations;
        }
        public async Task<Organization> GetOrganizationById(string userId, int id)
        {
            return _dbcontext.Organizations.Where(p => p.OrganizationId == id && p.UserId == userId).First();
        }

        public async Task CreateOrganization(Organization Organization)
        {
            if (Organization != null)
            {
                try{ await _dbcontext.AddAsync(Organization);}
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

        public async Task UpdateOrganization(Organization Organization)
        {
           //Nothing
        }
    
            

        public bool SaveChanges()
        {
            return (_dbcontext.SaveChanges() >= 0);
        }

       
    }
}
