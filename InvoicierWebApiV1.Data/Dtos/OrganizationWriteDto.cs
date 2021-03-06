using System.ComponentModel.DataAnnotations;

namespace InvoicierWebApiV1.Core.Dtos
{
    public class OrganizationWriteDto
    {
        [Required]
        public string Name { get; set; }
        public string Location { get; set; }

        [Required]
        public string Email { get; set; }
        public int PostalCode { get; set; }
        public string ImageLogo { get; set; }
        
        public virtual OrganizationWriteDtoAddress Address { get; set; }

    }

    public class OrganizationWriteDtoAddress
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public int Zipcode { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }
}
