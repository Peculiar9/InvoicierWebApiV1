using InvoicierWebApiV1.Core.Dtos.InvoiceDtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InvoicierWebApiV1.Dtos.InvoiceDtos
{
    public class InvoiceCreateDto
    {
        
        [Required]
        public string Price { get; set; }
        public string Discount { get; set;}
        public DateTime CreatedOn { get; set; }
        public DateTime ExpiredOn { get; set; }
        public string Comment { get; set; }
        public string Email { get; set; }
        public string Concept { get; set; } 
        [Required]
        public bool IsPaid { get; set; } = false;
        public string Total { get; set; } 
        public List<InvoiceItemsDTO> Items { get; set; }
    }
}