using InvoicierWebApiV1.Data.EntityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InvoicierWebApiV1.Dtos
{
    public class OrganizationUpdateDto
    {
        public string Name { get; set; }
        public string Location { get; set; }

        [Required]
        public string Email { get; set; }
        public int PostalCode { get; set; }
        public string ImageLogo { get; set; }
        
        public virtual OrganizationAddressUpdateDto Address { get; set; }
    }
    public class OrganizationAddressUpdateDto
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public int Zipcode { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }
}
