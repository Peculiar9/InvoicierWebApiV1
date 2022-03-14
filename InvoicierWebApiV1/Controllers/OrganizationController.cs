using AutoMapper;
using InvoicierWebApiV1.Core.Dtos;
using InvoicierWebApiV1.Core.EntityModels;
using InvoicierWebApiV1.Core.Interfaces.OrganizationServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvoicierWebApiV1.Controllers
{

    [ApiController]
    [Authorize(Roles = UserRoles.Admin)]
    [Route("api/Organizations")]
    public class OrganizationController : ControllerBase
    {
        private readonly IMapper _mapper; 
        private readonly IOrganizationServices _service;
        private readonly IWebHostEnvironment _webhost;

        public OrganizationController(IOrganizationServices services, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _mapper = mapper;
            _service = services;
            this._webhost = webHostEnvironment;
        }

        // GET api/Organization
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var organizations = await _service.GetOrganizations();
            var organizationsDto = _mapper.Map<List<OrganizationReadDto>>(organizations);
            return Ok(organizationsDto);
        }

        [Route("{id}", Name = "OrganizationById")]
        [HttpGet]
        public async Task<IActionResult> OrganizationById(int id)
        {
            var item = await _service.GetOrganizationById(id);
            if (item != null)
            {
                return Ok(_mapper.Map<OrganizationReadDto>(item));
            }
            return NotFound();

        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateOrganization(OrganizationWriteDto organizationWriteDto)
        {
            var organizationModel = _mapper.Map<Organization>(organizationWriteDto);

            await _service.CreateOrganization(organizationModel);
            if(!_service.SaveChanges()) return BadRequest();

            var organizationReadDto = _mapper.Map<OrganizationReadDto>(organizationModel);
            return CreatedAtRoute(nameof(OrganizationById), new { Id = organizationReadDto.OrganizationId }, organizationReadDto);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, OrganizationUpdateDto updateDto)
        {
            var organizationModelFromDto = await _service.GetOrganizationById(id);
            if (organizationModelFromDto == null)
            {
                return NotFound();
            }
            _mapper.Map(updateDto, organizationModelFromDto);

            await _service.UpdateOrganization(organizationModelFromDto);
            if (!_service.SaveChanges()) return BadRequest();
            return CreatedAtRoute(nameof(OrganizationById), new { Id = updateDto.Name }, updateDto); ;
        }
            
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var organizationFromRepo = await _service.GetOrganizationById(id);
            if (organizationFromRepo == null)
            {
                return NotFound();
            }
            await _service.DeleteOrganization(organizationFromRepo);
            if(!_service.SaveChanges()) return BadRequest();
            return Ok();
        }
    }   
}




