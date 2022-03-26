using AutoMapper;
using InvoicierWebApiV1.Core.Dtos;
using InvoicierWebApiV1.Core.EntityModels;
using InvoicierWebApiV1.Core.Interfaces;
using InvoicierWebApiV1.Core.Interfaces.OrganizationServices;
using InvoicierWebApiV1.Core.Interfaces.UseCases;
using InvoicierWebApiV1.Core.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoicierWebApiV1.Core.Services.UseCases
{
    public class OrganizationUseCase : IOrganizationUsecase
    {
        private readonly IOrganizationServices _service;
        private readonly IMapper _mapper;
        private readonly IClientService _clientService;
        public OrganizationUseCase(IOrganizationServices organizationService, IClientService clientServices, IMapper mapper)
        {
            _clientService = clientServices;
            _service = organizationService;
            _mapper = mapper;
        }
     

        public async Task<Response> GetOrganizations()
        {
            try
            {
            var organizations = await _service.GetOrganizations();
            var organizationsList = new List<OrganizationReadDto>();
            var client = await _clientService.GetClients();
            var clientsfromService = _mapper.Map<List<ClientReadDto>>(client);
         
            var clients = new List<ClientReadDto>();
            var address = new OrganizationAddress();
               
            if (organizations.ToList().Count < 1) 
                return new Response().failed("Failed");
            organizations.ToList().ForEach(x => {
                clients = clientsfromService.ToList().Select(client => client).Where(org => org.OrganizationId == x.OrganizationId).ToList();
                address = x.Address;
                organizationsList.Add(new OrganizationReadDto(x, clients, address));
            });
            var response = new List<OrganizationReadDto>();
            
            response.AddRange(organizationsList);
            return new Response().success("Successful", response);
            }
            catch (Exception err)
            {
                return new Response().failed(err.Message);
            }
        }
        public async Task<Response> GetOrganizationById(int id)
        {
            var response = new Response();
            var message = "";
            try
            {
                var organizationItem = await _service.GetOrganizationById(id);
                var clients = new List<ClientReadDto>();
                var clientsFromList = await _clientService.GetClients();
                var clientsfromService = _mapper.Map<List<ClientReadDto>>(clientsFromList);
                clients = clientsfromService.ToList().Select(client => client).Where(org => org.OrganizationId == organizationItem.OrganizationId).ToList();
                var organization = new OrganizationReadDto(organizationItem, clients, organizationItem.Address);
                if (organizationItem != null)
                {
                    return response.success("successful request", organization);
                }

            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return response.failed($"failed request: {message}");
         } 
           
        public async Task<Response> CreateOrganization(OrganizationWriteDto organizationWriteDto)
        {
            try
            {
            var response = new Response();
            var organizationModel = _mapper.Map<Organization>(organizationWriteDto);
            await _service.CreateOrganization(organizationModel);
            var organizationReadDto = _mapper.Map<OrganizationReadDto>(organizationModel); ;
            response.Data = organizationReadDto ?? new object();
                if (_service.SaveChanges()) response.StatusCode = 200; return response;
        
            }
            catch (Exception ex)
            {
                return new Response().failed(ex.InnerException.ToString(), null , ResponseType.ServerError);
                throw;
            }
            return new Response().failed($"Unable to save {organizationWriteDto.Name} try again later", null, ResponseType.ServerError);
        }
        public async Task<Response> DeleteOrganization(int organizationId)
        {
            Response response = new Response();
            try
            {
                var organizationFromRepo = await _service.GetOrganizationById(organizationId);
                if (organizationFromRepo == null)
                {
                    return response.failed("Organization Not Found", ResponseType.NotFound);
                }
                
                await _service.DeleteOrganization(organizationFromRepo);
                if (!_service.SaveChanges()) return response.failed("Not deleted successful try again!!", ResponseType.ServerError); 
                return response.success("Successfully deleted");
            }
            catch(Exception ex) { return response.failed(ex.Message, ResponseType.ServerError); };
        }
            
        
        public Task<Response> UpdateOrganization(int organizationId, OrganizationUpdateDto organizationModel)
        {
            throw new NotImplementedException();
        }

       
    }
}
    

  
