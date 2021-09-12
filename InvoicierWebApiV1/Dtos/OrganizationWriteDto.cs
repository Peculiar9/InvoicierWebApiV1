using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InvoicierWebApiV1.Dtos
{
    public class OrganizationWriteDto
    {
        [Required]
        public string Name { get; set; }
        public string Location { get; set; }
        public string Address { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
