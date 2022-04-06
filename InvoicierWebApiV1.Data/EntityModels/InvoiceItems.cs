using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicierWebApiV1.Core.EntityModels
{
    public class InvoiceItems
    {
        [Key]
        public int ItemId { get; set; }
        public string ItemDescription { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string Title { get; set; }
        [ForeignKey("Invoice")]
        public int InvoiceId {  get; set; }
        public virtual Invoice Invoice { get; set; }
    }
}
