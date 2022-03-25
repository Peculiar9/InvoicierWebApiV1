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
        public Task<Response> DeleteOrganization(string organizationId)
        {
            throw new NotImplementedException();
        }
            
        
        public Task<Response> UpdateOrganization(int organizationId, OrganizationUpdateDto organizationModel)
        {
            throw new NotImplementedException();
        }

       
    }
    
  
}
