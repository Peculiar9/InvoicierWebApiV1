using InvoicierWebApiV1.Core.Dtos;
using InvoicierWebApiV1.Core.Shared;
using System.Threading.Tasks;

namespace InvoicierWebApiV1.Core.Interfaces.UseCases
{
    public interface IOrganizationUsecase
    {
        Task<Response> GetOrganizations();
        Task<Response> CreateOrganization(OrganizationWriteDto organizationWriteDto);
        Task<Response> DeleteOrganization(string organizationId);
        Task<Response> UpdateOrganization(int organizationId, OrganizationUpdateDto organizationModel);
        
    }
}
