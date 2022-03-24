using AutoMapper;
using InvoicierWebApiV1.Core.Dtos;
using InvoicierWebApiV1.Core.EntityModels;
using InvoicierWebApiV1.Core.Interfaces.OrganizationServices;
using InvoicierWebApiV1.Core.Interfaces.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
            var response = _service.GetOrganizations();
            if (!response.IsCompleted)
              return NotFound(response);
            return Ok(response);
        }
            

        //[Route("{id}", Name = "OrganizationById")]
        //[HttpGet]
        //public async Task<IActionResult> OrganizationById(int id)
        ////{
        ////    var item = await _service.GetOrganizationById(id);
        ////    if (item != null)
        ////    {
        ////        return Ok(_mapper.Map<OrganizationReadDto>(item));
        ////    }
        //    return NotFound();

        //}
        //[HttpPost]
        //[Route("create")]
        //public async Task<IActionResult> CreateOrganization(OrganizationWriteDto organizationWriteDto)
        //{
        //    ////var organizationModel = _mapper.Map<Organization>(organizationWriteDto);
        //    var organizationModel =new OrganizationWriteDto();
        
        //    ////await _service.CreateOrganization(organizationModel);
        //    ////_service.SaveChanges();

        //    //var organizationReadDto = _mapper.Map<OrganizationReadDto>(organizationModel);
        //    //return CreatedAtRoute(nameof(OrganizationById), new { Id = organizationReadDto.OrganizationId }, organizationReadDto);
        //    return Ok(organizationModel);
        //}

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
