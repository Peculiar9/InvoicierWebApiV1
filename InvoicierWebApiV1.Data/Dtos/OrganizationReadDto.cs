using InvoicierWebApiV1.Core.EntityModels;
using System.ComponentModel.DataAnnotations;

namespace InvoicierWebApiV1.Core.Dtos
{
    public class OrganizationReadDto
    { [Key]
        public int OrganizationId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Location { get; set; }
        //public string Address { get; set; }
        [Required]
        public string Email { get; set; }
        public string ImageLogo { get; set; }
        [Required]
        public virtual OrganizationAddressReadDto Address { get; set; }
    }

    public class OrganizationAddressReadDto
    {
        [Key]
        public int Id { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public int Zipcode { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

        public virtual Organization Organization { get; set; }
    }
}
