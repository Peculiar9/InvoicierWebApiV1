using System.Threading.Tasks;
using AutoMapper;
using InvoicierWebApiV1.Core.EntityModels;
using InvoicierWebApiV1.Core.Interfaces;
using InvoicierWebApiV1.Dtos.InvoiceDtos;
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
        public IInvoiceService Service { get; }

        private IMapper _mapper;

        public InvoiceController(IInvoiceService service, IMapper mapper)
        {
            Service = service;
            _mapper = mapper;
        }
        

        // GET api/invoice/
        [HttpGet("")]
        public async Task<IActionResult> Index(){
            var model = await Service.GetInvoices();

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
        [Route("create")]
        public async Task<IActionResult> CreateInvoice([FromBody]InvoiceCreateDto invoiceModelDto)
        { 
            var invoiceModel = new Invoice {
                InvoiceNumber = invoiceModelDto.InvoiceNumber,
                Id = invoiceModelDto.Id,
                CreatedOn = invoiceModelDto.CreatedOn,
                ExpiredOn = invoiceModelDto.ExpiredOn,
                Comment = invoiceModelDto.Comment,
                Discount = invoiceModelDto.Discount,
                IsPaid = invoiceModelDto.IsPaid,
                Total = invoiceModelDto.Total,
                client = new Client{
                    Id = invoiceModelDto.client.Id,
                    FirstName = invoiceModelDto.client.FirstName,
                    LastName = invoiceModelDto.client.LastName,
                    Email = invoiceModelDto.client.Email,
                    BankAccount = invoiceModelDto.client.BankAccount,
                    OrganizationId = invoiceModelDto.client.OrganizationId
                }
            };
             await Service.CreateInvoice(invoiceModel);
             Service.SaveChanges();
             var invoiceReadDto = _mapper.Map<InvoiceReadDto>(invoiceModel);
           return CreatedAtRoute(nameof(GetInvoiceById), new {Id = invoiceReadDto.Id}, invoiceReadDto); 
        
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
        
        

