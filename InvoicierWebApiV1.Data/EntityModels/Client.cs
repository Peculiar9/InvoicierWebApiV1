using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoicierWebApiV1.Data.EntityModels
{
    public class Client
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "FirstName is required")]
        public string FirstName { get; set;}
        [Required(ErrorMessage = "LastName is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Organization is required")]
        [ForeignKey("OrganizationId")]

        public string Address { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        public string BankAccount { get; set; }
        public bool Status { get; set; }        
    }
}



