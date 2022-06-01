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
     
        
        public async Task<Response> GetAllOrganizations()
        {
            try
            {
            var organizations = await _service.GetAllOrganizations();
            var organizationsList = new List<OrganizationReadDto>();
         
            var clients = new List<ClientReadDto>();
            var address = new OrganizationAddress();
               
            if (organizations.ToList().Count < 1) 
                return new Response().success("Add Organization", new object());

            organizations.ToList().ForEach(async x => {
                var clientFromService = await _clientService.GetClientByOrgId(x.OrganizationId);
                clients = _mapper.Map<List<ClientReadDto>>(clientFromService);
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
        }  public async Task<Response> GetOrganizations(string userId)
        {
            try
            {
            var organizations = await _service.GetOrganizations(userId);
            var organizationsList = new List<OrganizationReadDto>();
         
            var clients = new List<ClientReadDto>();
            var address = new OrganizationAddress();
               
            if (organizations.ToList().Count < 1) 
                return new Response().success("Add Organization", new object());

            organizations.ToList().ForEach(async x => {
                var clientFromService = await _clientService.GetClientByOrgId(x.OrganizationId);
                clients = _mapper.Map<List<ClientReadDto>>(clientFromService);
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
                
        public async Task<Response> GetOrganizationById(string userId, int id)
        {
            var response = new Response();
            var message = "";
            try
            {
                var organizationItem = await _service.GetOrganizationById(userId, id);
                var clients = new List<ClientReadDto>();
                if (organizationItem == null) return response.failed("Organization does not exist");
                var clientsFromList = await _clientService.GetClientByOrgId(id);
                clients = _mapper.Map<List<ClientReadDto>>(clientsFromList);
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
           
        public async Task<Response> CreateOrganization(OrganizationWriteDto organizationWriteDto, string userId)
        {
            var response = new Response();
            try
            {
                var organizationModel = _mapper.Map<Organization>(organizationWriteDto);
                organizationModel.UserId = userId;
                await _service.CreateOrganization(organizationModel);
                var organizationReadDto = _mapper.Map<OrganizationReadDto>(organizationModel); ;
                response.Data = organizationReadDto ?? new object();
                if (!_service.SaveChanges())
                    return response.failed($"Organization {organizationWriteDto.Name} was not saved successfully");

            }
            catch (Exception ex)
            {
                return new Response().failed(ex.Message, null, ResponseType.ServerError);
            }
            return response.success("Organization saved successfully!!!"); 
        }

        public async Task<Response> UpdateOrganization(string userid, int organizationId, OrganizationUpdateDto organizationModel)
        {
            Response response = new Response();
            var organizationFromRepo = await _service.GetOrganizationById(userid, organizationId);
            if (organizationFromRepo == null)
            {
                return response.failed("Not Found");
            }
            _mapper.Map(organizationModel, organizationFromRepo);
            await _service.UpdateOrganization(organizationFromRepo);
            if (_service.SaveChanges()) return new Response().success("Successful Request", organizationId);
            else
            {
                return new Response().failed("Not Successful");
            }

        }
        public async Task<Response> DeleteOrganization(string userid, int organizationId)
        {
            Response response = new Response();
            try
            {
                var organizationFromRepo = await _service.GetOrganizationById(userid, organizationId);
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
            
        
      

       
    }
}
    

  
