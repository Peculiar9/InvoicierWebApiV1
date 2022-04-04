using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicierWebApiV1.Core.EntityModels
{
    public class InvoiceItems
    {
        public int ItemId { get; set; }
        public string ItemDescription { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public double Title { get; set; }
        [ForeignKey("InvoiceId")]
        public int InvoiceId {  get; set; }
    }
}
