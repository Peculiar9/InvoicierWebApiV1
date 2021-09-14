using AutoMapper;
using InvoicierWebApiV1.Data.EntityModels;
using InvoicierWebApiV1.Dtos;
using InvoicierWebApiV1.Service.OrganizationServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InvoicierWebApiV1.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/Organizations")]
    public class OrganizationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IOrganizationServices _service;
        public OrganizationController(IOrganizationServices services, IMapper mapper)
        {
            _mapper = mapper;
            _service = services;
        }
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var organizations = await _service.GetOrganizations();
            var organizationsDto = _mapper.Map<OrganizationReadDto>(organizations);
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
        [HttpPost("{id}")]
        public async Task<IActionResult> CreateOrganization(OrganizationWriteDto organizationWriteDto)
        {
            var organizationModel = _mapper.Map<Organization>(organizationWriteDto);
            await _service.CreateOrganization(organizationModel);
            _service.SaveChanges();

            var organizationReadDto = _mapper.Map<OrganizationReadDto>(organizationModel);
            return CreatedAtRoute(nameof(OrganizationById), new { Id = organizationReadDto.Id }, organizationReadDto);
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

            _service.SaveChanges();

            return NoContent();
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
            _service.SaveChanges();
            return Ok();
        }
       
    }   
}
