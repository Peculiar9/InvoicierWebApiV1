using System.Threading.Tasks;
using AutoMapper;
using InvoicierWebApiV1.Core.Dtos;
using InvoicierWebApiV1.Core.EntityModels;
using InvoicierWebApiV1.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InvoicierWebApiV1.Controllers
{
    [ApiController]
    [Route("api/client")]
    public class ClientController : ControllerBase
    {
         private readonly IClientService _service;
         private readonly IMapper _mapper;

        public ClientController(IClientService service, IMapper mapper)
         {
             _service = service;
             _mapper = mapper;
         }    
         
        [HttpGet]
        [Route("")]
         public async Task<IActionResult> Index()
         {
            try
            {
             var clients = await _service.GetClients();
             return Ok(clients);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new Response {  Message = ex.Message, Status = "Service Error"});
            }
         }


        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetClientsById(int id)
        {
            var clients = await _service.GetClientsById(id);
            if (clients != null)
            {
              return Ok(clients);
            }
            return NotFound(new Response { Message = "Client not found", Status="Null Request" });  


        }

        [HttpPost]
        [Route("")]
         public async Task<IActionResult> CreateClient([FromBody]ClientCreateDto clientModelDto)
        {
            var clientModel = _mapper.Map<Client>(clientModelDto);
            
             await _service.CreateClient(clientModel);
             await _service.SaveChanges();
             if(clientModel != null){
                 return Ok( new Response{
                     Status = "Successful",
                     Message = $"{clientModel.FirstName} Client Created Successfully"
                 });
             }
             return BadRequest();
         } 
    }
}
                