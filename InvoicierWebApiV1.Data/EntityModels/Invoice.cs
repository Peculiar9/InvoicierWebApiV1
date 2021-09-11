using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicierWebApiV1.Data.EntityModels
{
    public class Invoice
    {
        public int Id { get; set; }
        public bool Status { get; set; } 
        public string Price { get; set; }

    }
}
