using InvoicierWebApiV1.Core.EntityModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace InvoicierWebApiV1.Dtos.InvoiceDtos
{
    public class InvoiceCreateDto
    {
        
        [Key]
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public string Discount { get; set;}
        public DateTime CreatedOn { get; set; }
        public DateTime ExpiredOn { get; set; }
        public string Comment { get; set; }
        public string Concept { get; set; }
        [Required]
        public bool IsPaid { get; set; } 
        [Required]
        public string Price { get; set; }
        public string Total { get; set; }
        public Client client { get; set; }
    }
}