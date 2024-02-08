using AutoMapper;
using SNSYS.Challenger.Api.Contracts;
using SNSYS.Challenger.Domain.Entities;

namespace SNSYS.Challenger.Api.Profiles
{
    public class CustomerSupplierContactProfile : Profile
    {
        public CustomerSupplierContactProfile()
        {
            CreateMap<CustomerSupplierContact, CustomerSupplierContactRequest>();
            CreateMap<CustomerSupplierContactRequest, CustomerSupplierContact>();
        }
    }
}
