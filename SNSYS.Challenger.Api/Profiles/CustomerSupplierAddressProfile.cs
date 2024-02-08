using AutoMapper;
using SNSYS.Challenger.Api.Contracts;
using SNSYS.Challenger.Domain.Entities;

namespace SNSYS.Challenger.Api.Profiles
{
    public class CustomerSupplierAddressProfile : Profile
    {
        public CustomerSupplierAddressProfile()
        {
            CreateMap<CustomerSupplierAddress, CustomerSupplierAddressRequest>();
            CreateMap<CustomerSupplierAddressRequest, CustomerSupplierAddress>();

        }
    }
}
