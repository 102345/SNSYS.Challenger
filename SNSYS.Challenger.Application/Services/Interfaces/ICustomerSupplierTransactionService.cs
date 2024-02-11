using SNSYS.Challenger.Application.Contracts;
using SNSYS.Challenger.Domain.Entities;
using SNSYS.Challenger.Domain.Filter;

namespace SNSYS.Challenger.Application.Services.Interfaces
{
    public interface ICustomerSupplierTransactionService
    {
        Task<CustomerSupplier> GetByIdAsync(int id);
        Task CreateAsync(CustomerSupplierModel customerSupplierModel);
        Task UpdateAsync(CustomerSupplierModel customerSupplierModel);
        Task DeleteAsync(int id);
        Task<IEnumerable<CustomerSupplierData>> GetAllAsync(FilterCustomerSupplier filterCustomerSupplier);
    }
}
