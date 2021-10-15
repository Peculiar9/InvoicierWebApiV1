using System.ComponentModel.DataAnnotations;

namespace InvoicierWebApiV1.Dtos.InvoiceDtos
{
    public class InvoiceReadDto
    {
         public int Id { get; set; }
        [Required]
        public bool Status { get; set; } 
        [Required]
        public string Price { get; set; }
    }
}