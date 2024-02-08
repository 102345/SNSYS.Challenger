using AutoMapper;
using SNSYS.Challenger.Api.Contracts;
using SNSYS.Challenger.Domain.Entities;

namespace SNSYS.Challenger.Api.Profiles
{
    public class CustomerSupplierDataProfile :Profile
    {
        public CustomerSupplierDataProfile()
        {
            CreateMap<CustomerSupplierData, CustomerSupplierResponse>();
            CreateMap<CustomerSupplierResponse, CustomerSupplierData>();
        }
    }
}
