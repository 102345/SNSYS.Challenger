using AutoMapper;
using SNSYS.Challenger.Api.Contracts;
using SNSYS.Challenger.Domain.Filter;

namespace SNSYS.Challenger.Api.Profiles
{
    public class FilterCustomerSupplierProfile : Profile
    {
        public FilterCustomerSupplierProfile()
        {
            CreateMap<FilterCustomerSupplier, FilterCustomerSupplierRequest>();
            CreateMap<FilterCustomerSupplierRequest, FilterCustomerSupplier>();
        }
    }
}
