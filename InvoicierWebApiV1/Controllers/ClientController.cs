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

        [HttpPost]
        [Route("")]
         public async Task<IActionResult> CreateClient([FromBody]ClientCreateDto clientModelDto)
        { 
            var clientModel = new Client {
                FirstName = clientModelDto.FirstName,
                Email = clientModelDto.Email,
                BankAccount = clientModelDto.BankAccount,
                Status = clientModelDto.Status,
                Address = clientModelDto.Address,
                OrganizationId = clientModelDto.OrganizationId,
                
            };
             await _service.CreateClient(clientModel);
             await _service.SaveChanges();
             var clientReadDto = _mapper.Map<ClientReadDto>(clientModel);
             if(clientReadDto != null){
                 return Ok( new Response{
                     Status = "Successful",
                     Message = "User Created Succesfully"
                 });
             }
             return NotFound();
         } 
    }
}