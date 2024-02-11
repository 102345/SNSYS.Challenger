using SNSYS.Challenger.Domain.Entities;
using SNSYS.Challenger.Domain.Filter;

namespace SNSYS.Challenger.Domain.Repositories.CustomerSupplierTransactionRepository
{
    public  interface ICustomerSupplierTransactionRepository
    {
        Task<CustomerSupplier> GetByIdAsync(int id);
        Task CreateAsync(CustomerSupplierOrchestrator customerSupplierOrchestrator);
        Task UpdateAsync(CustomerSupplierOrchestrator customerSupplierOrchestrator);
        Task DeleteAsync(int id);
        Task<IEnumerable<CustomerSupplierData>> GetAllAsync(FilterCustomerSupplier filterCustomerSupplier);
    }
}
