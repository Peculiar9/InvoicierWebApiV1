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
        public string Email { get; set; }
        //public virtual OrganizationAddressUpdateDto OrganizationAddress { get; set; }
    }
    public class OrganizationAddressUpdateDto
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public int Zipcode { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

        public virtual OrganizationUpdateDto Organization { get; set; }
    }
}
