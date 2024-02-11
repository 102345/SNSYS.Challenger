using SNSYS.Challenger.Domain.Entities;
using SNSYS.Challenger.InfraStructure.Data.Context;

namespace SNSYS.Challenger.InfraStructure.Interfaces.Repositories
{
    public interface ICustomerSupplierAddressRepository
    {
        Task<CustomerSupplierAddress> GetByIdAsync(int id, ChallengerSNSYSDbContext dbContext);
        Task CreateAsync(CustomerSupplierAddress customerSupplierAddress, ChallengerSNSYSDbContext dbContext);
        Task UpdateAsync(CustomerSupplierAddress customerSupplierAddress, ChallengerSNSYSDbContext dbContext);
        Task DeleteAsync(int id, ChallengerSNSYSDbContext dbContext);

        Task DeletePerCustomerSupplier(int customerSupplierId, ChallengerSNSYSDbContext dbContext);
    }
}
