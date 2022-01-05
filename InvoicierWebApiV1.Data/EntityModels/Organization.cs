using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoicierWebApiV1.Data.EntityModels
{
    public class Organization
    {
        [Key]
        public int OrganizationId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Location { get; set; }
        
        [Required]
        public string Email { get; set; }
        //public string PostalCode { get; set; }

        [Required]
        [ForeignKey("AddressId")]
        public virtual OrganizationAddress Address { get; set; }
        public string ImageLogo { get; set; }
        public ICollection<Client> Clients { get; set; }
   }
}
