using SNSYS.Challenger.Domain.Services.Interfaces;
using SNSYS.Challenger.Domain.Entities;
using SNSYS.Challenger.Domain.Repositories.CustomerSupplierRepository;
using SNSYS.Challenger.Domain.Filter;

namespace SNSYS.Challenger.Domain.Services.CustomerSupplierService
{
    public class CustomerSupplierService : ICustomerSupplierService
    {
        public ICustomerSupplierRepository _customerSupplierRepository;
        public CustomerSupplierService(ICustomerSupplierRepository customerSupplierRepository)
        {
            _customerSupplierRepository = customerSupplierRepository ?? throw new ArgumentNullException(nameof(customerSupplierRepository));
        }

        public async Task CreateAsync(CustomerSupplier customerSupplier)
        {
            await _customerSupplierRepository.CreateAsync(customerSupplier);
        }

        public async Task DeleteAsync(int id)
        {
           await _customerSupplierRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<CustomerSupplier>> GetAllAsync()
        {
            return await _customerSupplierRepository.GetAllAsync();
        }

        public async Task<IEnumerable<CustomerSupplierData>> GetAllAsync(FilterCustomerSupplier filterCustomerSupplier)
        {
            return await  _customerSupplierRepository.GetAllAsync(filterCustomerSupplier);
        }

        public async Task<CustomerSupplier> GetByIdAsync(int id)
        {
           return await _customerSupplierRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(CustomerSupplier customerSupplier)
        {
            await _customerSupplierRepository.UpdateAsync(customerSupplier);
        }
    }
}
