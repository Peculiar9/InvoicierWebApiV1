using AutoMapper;
using InvoicierWebApiV1.Core.Dtos;
using InvoicierWebApiV1.Core.Interfaces.OrganizationServices;
using InvoicierWebApiV1.Core.Interfaces.UseCases;
using InvoicierWebApiV1.Data.AuthModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<ApplicationUser> userManager;

        public OrganizationController(IOrganizationUsecase services, IMapper mapper, IWebHostEnvironment webHostEnvironment, IOrganizationServices dbService, UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _service = services;
            _dbService = dbService;
            this._webhost = webHostEnvironment;
            this.userManager = userManager;
        }

        [HttpGet, Route("get-all")]
        public async Task<IActionResult> Organization()
        {
            var user = await userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            var item = await _service.GetOrganizations(user.Id);
            return Ok(item);
        } 
        [HttpGet]
        public async Task<IActionResult> Organizations()
        {
            var user = await userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            var item = await _service.GetOrganizations(user.Id);
            return Ok(item);
        }
       
              
        [HttpGet]
        [Route("{id}", Name = "OrganizationById")]
        public async Task<IActionResult> OrganizationById(int id)
        {
            var user = await userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            var org = await _service.GetOrganizationById(user.Id, id);
            return Ok(org);
        }

        
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateOrganization(OrganizationWriteDto organizationWriteDto)
        {
            var user =await  userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            var response = await _service.CreateOrganization(organizationWriteDto, user.Id);        
            if (response.StatusCode == 200)
            {
                return Created(nameof(OrganizationById), response);
            }
            response.Message = "Could not save Organization try again later.";
            return BadRequest(response);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, OrganizationUpdateDto updateDto)
        {
            var user = await userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            var response =  await _service.UpdateOrganization(user.Id, id, updateDto);
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
            var user = await userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            var response = await _service.DeleteOrganization(user.Id, id);
                return Ok(response);
        }
     }
    }
       

            

           
