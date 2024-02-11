using SNSYS.Challenger.Domain.Entities;
using SNSYS.Challenger.InfraStructure.Data.Context;

namespace SNSYS.Challenger.InfraStructure.Interfaces.Repositories
{
    public interface ICustomerSupplierRepository
    {
        Task<CustomerSupplier> GetByIdAsync(int id, ChallengerSNSYSDbContext dbContext);
        Task<int> CreateAsync(CustomerSupplier customerSupplier, ChallengerSNSYSDbContext dbContext);
        Task UpdateAsync(CustomerSupplier customerSupplier, ChallengerSNSYSDbContext dbContext);
        Task DeleteAsync(int id, ChallengerSNSYSDbContext dbContext);
    }
}
