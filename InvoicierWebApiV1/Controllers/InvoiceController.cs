using System.Threading.Tasks;
using AutoMapper;
using InvoicierWebApiV1.Data.EntityModels;
using InvoicierWebApiV1.Dtos.InvoiceDtos;
using InvoicierWebApiV1.Services;
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
        public InvoiceService Service { get; }

        private IMapper _mapper;

        public InvoiceController(InvoiceService service, IMapper mapper)
        {
            Service = service;
            _mapper = mapper;
        }
        

        // GET api/invoice/
        [HttpGet("")]
        public IActionResult Index(){
            var model = Service.GetInvoices();
            return Ok(model);
        }
        ///<Summary>
        ///Create with {Name}
        ///</Summary>
        // GET api/invoice/{id}
        [HttpGet]
        [Route("{id}", Name = "GetHostelById")]
        public async Task<IActionResult> GetInvoiceById(int id){
             var invoice = await Service.GetInvoiceById(id);
            if(invoice != null)
            {
                return Ok(_mapper.Map<Invoice>(invoice));
            }
            return NotFound();
        }

        [HttpPost]
        [Route("create/{id}")]
        public async Task<IActionResult> CreateInvoice(InvoiceCreateDto invoiceCreate)
        { 
             var invoiceModel = _mapper.Map<Invoice>(invoiceCreate);
             await Service.CreateInvoice(invoiceModel);
             Service.SaveChanges();
             var hostelReadDto = _mapper.Map<InvoiceReadDto>(invoiceModel);
           return CreatedAtRoute(nameof(GetInvoiceById), new {Id = hostelReadDto.Id}, hostelReadDto); 
        }


        [HttpDelete]
        [Route("remove")]
        public async Task<IActionResult> DeleteInvoice(int id)
        { 
        var invoiceModelFromRepo = await Service.GetInvoiceById(id);
         if (invoiceModelFromRepo == null)
         {
             return NotFound();
         }
         await Service.RemoveInvoice(invoiceModelFromRepo);
           Service.SaveChanges();
           if(Service.SaveChanges() == true){
            return Ok( new Response{
                Status = "Successful",
                Message = "Invoice Deleted Successfully"
            });
           }
           else{
               return new StatusCodeResult(401);
           }

        }
    }
}
        
        

