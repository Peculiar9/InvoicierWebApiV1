using System.ComponentModel.DataAnnotations;

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
        public string ImageLogo { get; set; }

        [Required]
        public virtual OrganizationAddress Address { get; set; }


    }
}
