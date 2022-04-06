using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoicierWebApiV1.Core.EntityModels
{
    public class Organization
    {
        public int OrganizationId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Location { get; set; }
        
        [Required]
        public string Email { get; set; }
        //public string PostalCode { get; set; }
        public string ImageLogo { get; set; }

        [Required]
        [ForeignKey("AddressId")]
        public virtual OrganizationAddress Address { get; set; }


    }
}
