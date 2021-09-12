using InvoicierWebApiV1.Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicierWebApiV1.Service.OrganizationServices
{
    public interface IOrganizationServices
    {
        Task<IEnumerable<Organization>> GetOrganizations();
        Task<Organization> GetOrganizationById(int id);
        Task CreateOrganization(Organization Organization);
        Task UpdateOrganization(Organization Organization);
        Task DeleteOrganization(Organization Organization);
        bool SaveChanges();
    }
}
