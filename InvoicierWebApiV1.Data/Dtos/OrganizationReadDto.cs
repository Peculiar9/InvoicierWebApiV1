using InvoicierWebApiV1.Core.EntityModels;
using InvoicierWebApiV1.Core.Interfaces;
using InvoicierWebApiV1.Core.Interfaces.OrganizationServices;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InvoicierWebApiV1.Core.Dtos
{
    public class OrganizationReadDto

    {
        private readonly IClientService _services;
        
        public OrganizationReadDto(Organization organization, List<ClientReadDto> clientList, OrganizationAddress address)
        {
            Clients = clientList;
            OrganizationId = organization.OrganizationId;
            Name = organization.Name;
            Location = organization.Location; 
            Email = organization.Email;
            ImageLogo = organization.ImageLogo;
            Address = address;
        }
        
        
        
        [Key]
        public int OrganizationId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Location { get; set; }
        [Required]
        public List<ClientReadDto> Clients { get; set; }   
        public string Email { get; set; }
        public string ImageLogo { get; set; }
        public object Address { get; set; }
    }

  
}
