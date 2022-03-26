using InvoicierWebApiV1.Dtos.InvoiceDtos;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoicierWebApiV1.Core.EntityModels
{
    public class Invoice
    {
        public Invoice(InvoiceCreateDto invoice)
        {
           
            InvoiceNumber = invoice.InvoiceNumber;
            Discount = invoice.Discount;
            CreatedOn = invoice.CreatedOn;
            ExpiredOn = invoice.ExpiredOn;
            Comment = invoice.Comment ?? "";
            Concept = invoice.Concept ?? "";
            IsPaid = invoice.IsPaid;
            Price = invoice.Price;
            Total = invoice.Total;
            OrganizationId = invoice.OrganizationId;

        }
        public Invoice()
        {

        }
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
        [ForeignKey("Organization")]
        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }
        public Client client { get; set; }
    }
}

        
