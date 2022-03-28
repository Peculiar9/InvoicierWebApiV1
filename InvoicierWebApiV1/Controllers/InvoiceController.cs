using System.Threading.Tasks;
using AutoMapper;
using InvoicierWebApiV1.Core.EntityModels;
using InvoicierWebApiV1.Core.Interfaces.UseCases;
using InvoicierWebApiV1.Dtos.InvoiceDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace InvoicierWebApiV1.Controllers

{
    [Authorize(Roles = UserRoles.Admin)]
    [ApiController]
    [Route("api/invoice")]
    public class InvoiceController : ControllerBase
    {
        public readonly IInvoiceUseCase _service;

        private IMapper _mapper;

        public InvoiceController(IInvoiceUseCase service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }


        // GET api/invoice/
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
           var response = await _service.GetInvoices();
           if (response.StatusCode == 200)
           {
               return Ok(response);
           }
           return NotFound(response);
            
        }
   
       
    
       [HttpGet]
       [Route("{id}", Name = "GetInvoiceById")]
       public async Task<IActionResult> GetInvoiceById(int id)
       {
           var invoice = await _service.GetInvoiceById(id);
           if (invoice != null)
           {
               return Ok(_mapper.Map<Invoice>(invoice));
           }
           return NotFound();
       }


       [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateInvoice([FromBody] InvoiceCreateDto invoiceModelDto)
        {
            var response = await _service.CreateInvoice(invoiceModelDto);
            var data = invoiceModelDto;
             if(response.StatusCode == 200) return Created(nameof(InvoiceCreateDto.clientId), data);
            if (!string.IsNullOrEmpty(((InvoiceCreateDto)response.Data).Email)) 
                _service.MailInvoices(((InvoiceCreateDto)response.Data).Email);
            return BadRequest(response);    
        }

        
             



        //[HttpDelete]
        //[Route("remove")]
        //public async Task<IActionResult> DeleteInvoice(int id)
        //{ 
        //var invoiceModelFromRepo = await Service.GetInvoiceById(id);
        // if (invoiceModelFromRepo == null)
        // {
        //     return NotFound();
        // }
        // await Service.RemoveInvoice(invoiceModelFromRepo);
        //   Service.SaveChanges();
        //   if(Service.SaveChanges() == true){
        //    return Ok( new Response{
        //        Status = "Successful",
        //        Message = "Invoice Deleted Successfully"
        //    });
        //   }
        //   else{
        //       return new StatusCodeResult(401);
        //   }

        //}
    }
    }

        
        

