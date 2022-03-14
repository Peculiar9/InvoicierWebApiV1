using InvoicierWebApiV1.Data.AuthModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InvoicierWebApiV1.Core.Dtos
{
    public class ChangePasswordDto
    {
        public LoginModel User { get; set; }
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Provide Previous Password")]
        public string PreviousPassword { get; set; }
    }
}
