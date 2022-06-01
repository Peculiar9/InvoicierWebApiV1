using InvoicierWebApiV1.Core.Dtos;
using InvoicierWebApiV1.Core.EntityModels;
using InvoicierWebApiV1.Core.Shared;
using System.Threading.Tasks;

namespace InvoicierWebApiV1.Core.Interfaces.UseCases
{
    public interface IOrganizationUsecase
    {
        Task<Response> GetAllOrganizations();
        Task<Response> GetOrganizations(string userId);
        Task<Response> GetOrganizationById(string userid, int id);
        Task<Response> CreateOrganization(OrganizationWriteDto organizationWriteDto, string userId);
        Task<Response> DeleteOrganization(string userid, int organizationId);
        Task<Response> UpdateOrganization(string userid, int id, OrganizationUpdateDto organizationModel);
        
    }
}
