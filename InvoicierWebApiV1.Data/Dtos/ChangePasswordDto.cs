using InvoicierWebApiV1.Data.AuthModels;
using System.ComponentModel.DataAnnotations;

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
