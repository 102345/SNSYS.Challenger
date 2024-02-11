using SNSYS.Challenger.Domain.Entities;
using SNSYS.Challenger.InfraStructure.Data.Context;

namespace SNSYS.Challenger.InfraStructure.Interfaces.Repositories
{
    public  interface ICustomerSupplierContactRepository
    {
        Task<CustomerSupplierContact> GetByIdAsync(int id, ChallengerSNSYSDbContext dbContext);
        Task CreateAsync(CustomerSupplierContact customerSupplierContact, ChallengerSNSYSDbContext dbContext);
        Task UpdateAsync(CustomerSupplierContact customerSupplierContact, ChallengerSNSYSDbContext dbContext);
        Task DeleteAsync(int id, ChallengerSNSYSDbContext dbContext);
    }
}
