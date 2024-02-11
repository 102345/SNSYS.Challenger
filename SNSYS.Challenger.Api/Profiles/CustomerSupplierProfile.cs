using AutoMapper;
using SNSYS.Challenger.Api.Contracts;
using SNSYS.Challenger.Application.Contracts;

namespace SNSYS.Challenger.Api.Profiles
{
    public class CustomerSupplierProfile: Profile
    {
        public CustomerSupplierProfile()
        {
            CreateMap<CustomerSupplierModel, CustomerSupplierRequest>();
            CreateMap<CustomerSupplierRequest, CustomerSupplierModel>();
        }
    }
}
