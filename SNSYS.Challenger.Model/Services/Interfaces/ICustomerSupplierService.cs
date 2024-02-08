using SNSYS.Challenger.Domain.Entities;
using SNSYS.Challenger.Domain.Filter;

namespace SNSYS.Challenger.Domain.Services.Interfaces
{
    public interface ICustomerSupplierService
    {
        Task<CustomerSupplier> GetByIdAsync(int id);
        Task<IEnumerable<CustomerSupplier>> GetAllAsync();
        Task CreateAsync(CustomerSupplier customerSupplier);
        Task UpdateAsync(CustomerSupplier customerSupplier);
        Task DeleteAsync(int id);
        Task<IEnumerable<CustomerSupplierData>> GetAllAsync(FilterCustomerSupplier filterCustomerSupplier);
    }
}
