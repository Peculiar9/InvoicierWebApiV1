using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicierWebApiV1.Data.AuthModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
