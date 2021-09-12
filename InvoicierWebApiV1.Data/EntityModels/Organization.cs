﻿using System.ComponentModel.DataAnnotations;

namespace InvoicierWebApiV1.Data.EntityModels
{
    public class Organization
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Location { get; set; }
        public string Address { get; set; }
        [Required]
        public string Email { get; set; }


    }
}
