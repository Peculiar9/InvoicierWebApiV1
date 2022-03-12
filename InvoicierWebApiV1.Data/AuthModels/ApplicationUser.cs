using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicierWebApiV1.Data.AuthModels
{

    public class ApplicationUser : IdentityUser
    {
        [Key]
        public int UserId { get; set; }
        //public string UserName { get; set; } 
        public string UserRoleId { get; set; }
        public string Password { get; set; }
    }
}
