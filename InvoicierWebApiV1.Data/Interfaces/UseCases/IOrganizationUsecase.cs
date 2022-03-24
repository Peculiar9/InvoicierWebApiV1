using InvoicierWebApiV1.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicierWebApiV1.Core.Interfaces.UseCases
{
    public interface IOrganizationUsecase
    {
        Task<bool> GetOrganizations();
        Task<bool> CreateOrganization();
        Task<bool> DeleteOrganization(string organizationId);
        Task<bool> UpdateOrganization(int organizationId, OrganizationUpdateDto organizationModel);
        
    }
}
