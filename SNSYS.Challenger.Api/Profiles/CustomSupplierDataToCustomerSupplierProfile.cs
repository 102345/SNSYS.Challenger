using AutoMapper;
using SNSYS.Challenger.Api.Contracts;
using SNSYS.Challenger.Domain.Entities;

namespace SNSYS.Challenger.Api.Profiles
{
    public class CustomSupplierDataToCustomerSupplierProfile : Profile
    {
        public CustomSupplierDataToCustomerSupplierProfile()
        {
            CreateMap<CustomerSupplierData, CustomerSupplierResponse>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
                .ForMember(d => d.Type, o => o.MapFrom(s => s.Type))
                .ForMember(d => d.DocumentNumber, o => o.MapFrom(s => s.DocumentNumber))

                .ForPath(d => d.Addresses.Address, o => o.MapFrom(s => s.Address))
                .ForPath(d => d.Addresses.City, o => o.MapFrom(s => s.City))
                .ForPath(d => d.Addresses.Country, o => o.MapFrom(s => s.Country))
                .ForPath(d => d.Addresses.ZIP, o => o.MapFrom(s => s.ZIP))
                .ForPath(d => d.Contacts.Name, o => o.MapFrom(s => s.ContactName))
                .ForPath(d => d.Contacts.Email, o => o.MapFrom(s => s.ContactEmail))
                .ForPath(d => d.Contacts.PhoneNumber, o => o.MapFrom(s => s.ContactPhoneNumber))
                .ForPath(d => d.Contacts.Position, o => o.MapFrom(s => s.ContactPosition));

        }
    }
}
