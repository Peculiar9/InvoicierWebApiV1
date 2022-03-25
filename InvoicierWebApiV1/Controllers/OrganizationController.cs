using AutoMapper;
using InvoicierWebApiV1.Core.Dtos;
using InvoicierWebApiV1.Core.Interfaces.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
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

        public OrganizationController(IOrganizationUsecase services, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _mapper = mapper;
            _service = services;
            this._webhost = webHostEnvironment;
        }

        // GET 
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var response = await _service.GetOrganizations();
            if (response.StatusCode == 200)
              return Ok(response);
            return NotFound(response);
        }


        [HttpGet]
        [Route("{id}", Name = "OrganizationById")]
        public async Task<IActionResult> OrganizationById(int id)
        {
            var item = await _service.GetOrganizations();
            if (item.StatusCode == 200)
            {
                var org = ((IEnumerable<OrganizationReadDto>)item.Data).FirstOrDefault(c => c.OrganizationId == id);
                return Ok(org);
            }
            return NotFound();
        }
        
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateOrganization(OrganizationWriteDto organizationWriteDto)
        {
            var response = await _service.CreateOrganization(organizationWriteDto);
            var organizationRead = new OrganizationReadDto();
            if (response.StatusCode == 200)
            {
                return Created(nameof(OrganizationById), (OrganizationReadDto)response.Data);
            }
            response.Message = "Could not save Organization try again later.";
            return BadRequest(response);
        }
       
       

       
            //[HttpPut("{id}")]
            //[Authorize]
            //public async Task<IActionResult> Update(int id, OrganizationUpdateDto updateDto)
            //{
            //    var organizationModelFromDto = await _service.GetOrganizationById(id);
            //    if (organizationModelFromDto == null)
            //    {
            //        return NotFound();
            //    }
            //    _mapper.Map(updateDto, organizationModelFromDto);

            //    await _service.UpdateOrganization(organizationModelFromDto);

            //    _service.SaveChanges();

            //    return NoContent();
            //}

            //[Authorize]
            //[HttpDelete("{id}")]
            //public async Task<IActionResult> Delete(int id)
            //{
            //    var organizationFromRepo = await _service.GetOrganizationById(id);
            //    if (organizationFromRepo == null)
            //    {
            //        return NotFound();
            //    }
            //    await _service.DeleteOrganization(organizationFromRepo);
            //    _service.SaveChanges();
            //    return Ok();
            //}


        }
    }
