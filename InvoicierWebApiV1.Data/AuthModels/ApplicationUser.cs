using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace InvoicierWebApiV1.Data.AuthModels
{

    public class ApplicationUser : IdentityUser
    {
        [Key]
        public int UserId { get; set; }
        public string UserRoleId { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
