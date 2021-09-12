using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoicierWebApiV1.Dtos
{
    public class OrganizationUpdateDto
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
    }
}
