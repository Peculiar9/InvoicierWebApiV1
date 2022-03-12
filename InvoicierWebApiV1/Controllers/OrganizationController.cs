﻿using AutoMapper;
using InvoicierWebApiV1.Data.EntityModels;
using InvoicierWebApiV1.Dtos;
using InvoicierWebApiV1.Service.OrganizationServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvoicierWebApiV1.Controllers
{

    [ApiController]
    //[Authorize(Roles = UserRoles.Admin)]
    //[Authorize]
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

        // GET 
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
            _service.SaveChanges();

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



        //Image Upload Controller


        //public async Task<string> ImageUpload(IFormFile image, HttpContext httpContext)
        //{
        //    string fileName = new string(
        //       Path.GetFileNameWithoutExtension(image.FileName)
        //       .Take(10)
        //       .ToArray())
        //       .Replace(' ', '-');
        //    fileName = fileName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(image.FileName);
        //    var imagePath = Path.Combine(_webhost.ContentRootPath, "Images", fileName);
        //    using (var fileStream = new FileStream(imagePath, FileMode.Create))
        //    {
        //        await image.CopyToAsync(fileStream);
        //    }
        //    return fileName;
          
        //}
    }   
}
