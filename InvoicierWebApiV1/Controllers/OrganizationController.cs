using AutoMapper;
using InvoicierWebApiV1.Core.Dtos;
using InvoicierWebApiV1.Core.Interfaces.OrganizationServices;
using InvoicierWebApiV1.Core.Interfaces.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InvoicierWebApiV1.Controllers
{

    [ApiController]
    [Authorize(Roles = UserRoles.Admin)]
    //[Authorize]
    [Route("api/Organizations")]
    public class OrganizationController : ControllerBase
    {
        private readonly IMapper _mapper; 
        private readonly IOrganizationUsecase _service;
        private readonly IWebHostEnvironment _webhost;
        private readonly IOrganizationServices _dbService;
        public OrganizationController(IOrganizationUsecase services, IMapper mapper, IWebHostEnvironment webHostEnvironment, IOrganizationServices dbService)
        {
            _mapper = mapper;
            _service = services;
            _dbService = dbService;
            this._webhost = webHostEnvironment;
        }

        // GET 
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var response = await _service.GetOrganizations();
            if (response.StatusCode != 200)
              return BadRequest(response);
            response.Message = "No existing Organization Please Add";
            return Ok(response);
        }


        [HttpGet]
        [Route("{id}", Name = "OrganizationById")]
        public async Task<IActionResult> OrganizationById(int id)
        {
            var item = await _service.GetOrganizations();
            if (item.StatusCode == 200)
            {
                var org = await _service.GetOrganizationById(id);
                
                return Ok(org);
            }
            return NotFound();
        }
        
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateOrganization(OrganizationWriteDto organizationWriteDto)
        {
            var response = await _service.CreateOrganization(organizationWriteDto);        
            if (response.StatusCode == 200)
            {
                return Created(nameof(OrganizationById), (OrganizationReadDto)response.Data);
            }
            response.Message = "Could not save Organization try again later.";
            return BadRequest(response);
        }




        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, OrganizationUpdateDto updateDto)
        {
           var response =  await _service.UpdateOrganization(id, updateDto);
            if (response.StatusCode == 404)
            {
                return NotFound();
            }
            else if(response.StatusCode == 200)
            {
                return NoContent();
            }
            else
            {
                return BadRequest(response);
            }
        }

        //[HttpPut, Route("api/update")]
        //public async Task<IActionResult> UpdateOrg(int id, OrganizationUpdateDto organization)
        //{
        //    var org = await _dbService.GetOrganizationById(id);
        //    if (org == null)
        //    {
        //        return NotFound();
        //    }
        //    _mapper.Map(organization, org);
        //    if(!_dbService.SaveChanges()) return BadRequest();
        //    else return NoContent();

        //}



        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _service.DeleteOrganization(id);
                return Ok(response);
        }
     }
    }
       

            

           
