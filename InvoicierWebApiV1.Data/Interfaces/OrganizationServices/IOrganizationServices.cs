using InvoicierWebApiV1.Core.EntityModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvoicierWebApiV1.Core.Interfaces.OrganizationServices
{
    public interface IOrganizationServices
    {
        Task<IEnumerable<Organization>> GetOrganizations();
        Task<IEnumerable<OrganizationAddress>> GetOrganizationAddresses();
        Task<Organization> GetOrganizationById(int id);
        Task CreateOrganization(Organization Organization);
        Task UpdateOrganization(Organization Organization);
        Task DeleteOrganization(Organization Organization);
        bool SaveChanges();
    }
}
