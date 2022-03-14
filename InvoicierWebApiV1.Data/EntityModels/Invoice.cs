using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicierWebApiV1.Core.EntityModels
{
    public class Invoice
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
        [ForeignKey("Organization")]
        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }
        public Client client { get; set; }
    }
}

        
