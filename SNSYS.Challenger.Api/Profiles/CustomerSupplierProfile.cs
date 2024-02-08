using AutoMapper;
using SNSYS.Challenger.Api.Contracts;
using SNSYS.Challenger.Domain.Entities;

namespace SNSYS.Challenger.Api.Profiles
{
    public class CustomerSupplierProfile: Profile
    {
        public CustomerSupplierProfile()
        {
            CreateMap<CustomerSupplier, CustomerSupplierRequest>();
            CreateMap<CustomerSupplierRequest, CustomerSupplier>();
        }
    }
}
