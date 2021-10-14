using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvoicierWebApiV1.Controllers

{
    [Authorize(Roles = UserRoles.Admin)]
    [Authorize]
    [ApiController]
    [Route("api/invoice")]
    public class InvoiceController : ControllerBase
    {
        
        [HttpGet("")]
        public IActionResult Index(){
            return Ok();
        }

        [HttpPost]
        [Route("create/{id}")]
        public IActionResult CreateInvoice(){ 
            return Ok();
        }
    }
}
        