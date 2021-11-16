using System.ComponentModel.DataAnnotations;

namespace InvoicierWebApiV1.Dtos
{
    public class ClientCreateDto
    {
         [Key]  
        public int Id { get; set; }
        [Required(ErrorMessage = "FirstName is required")]
        public string FirstName { get; set;}
        [Required(ErrorMessage = "LastName is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Organization is required")]
        public int OrganizationId { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        public string BankAccount { get; set; }
        public bool Status { get; set; }        
    }
}