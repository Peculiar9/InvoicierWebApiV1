using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicierWebApiV1.Core.Dtos.InvoiceDtos
{
    public class InvoiceItemsReadDTO
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string Title { get; set; }
        public int InvoiceId { get; set; }
    }
}
