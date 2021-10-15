using AutoMapper;
using InvoicierWebApiV1.Data.EntityModels;
using InvoicierWebApiV1.Dtos;
using InvoicierWebApiV1.Dtos.InvoiceDtos;

namespace InvoicierWebApiV1.Profiles
{
    public class OrganizationsProfile : Profile
    {
        public OrganizationsProfile()
        {
            //Source => Destination
            CreateMap<Organization, OrganizationReadDto>();
            CreateMap<OrganizationWriteDto, Organization>();
            CreateMap<OrganizationUpdateDto, Organization>();
            CreateMap<Organization, OrganizationUpdateDto>();
            CreateMap<Invoice, InvoiceReadDto>();
            
        }
    }
}
