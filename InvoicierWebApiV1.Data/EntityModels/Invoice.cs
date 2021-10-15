using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicierWebApiV1.Data.EntityModels
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public bool Status { get; set; } 
        [Required]
        public string Price { get; set; }
        

    }
}
