using InvoicierWebApiV1.Core.Dtos;
using InvoicierWebApiV1.Core.Shared;
using System.Threading.Tasks;

namespace InvoicierWebApiV1.Core.Interfaces.UseCases
{
    public interface IOrganizationUsecase
    {
        Task<Response> GetOrganizations();
        Task<bool> CreateOrganization();
        Task<bool> DeleteOrganization(string organizationId);
        Task<bool> UpdateOrganization(int organizationId, OrganizationUpdateDto organizationModel);
        
    }
}
