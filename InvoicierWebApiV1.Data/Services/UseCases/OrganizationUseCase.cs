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
        public Task<bool> CreateOrganization()
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteOrganization(string organizationId)
        {
            throw new NotImplementedException();
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
        
        public Task<bool> UpdateOrganization(int organizationId, OrganizationUpdateDto organizationModel)
        {
            throw new NotImplementedException();
        }

       
    }
    
  
}
