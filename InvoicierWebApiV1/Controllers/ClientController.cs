using System.Threading.Tasks;
using AutoMapper;
using InvoicierWebApiV1.Data.EntityModels;
using InvoicierWebApiV1.Dtos;
using InvoicierWebApiV1.Services;
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
             var clients = await _service.GetClients();
             return Ok(clients);
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
                     Message = $"{clientModel.FirstName} User Created Succesfully"
                 });
             }
             return NotFound();
         } 
    }
}
                