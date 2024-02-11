using SNSYS.Challenger.Domain.Entities;
using SNSYS.Challenger.InfraStructure.Data.Context;
using SNSYS.Challenger.InfraStructure.Interfaces.Repositories;

namespace SNSYS.Challenger.InfraStructure.Repositories.CustomerSupplierAddressRepository
{
    public class CustomerSupplierAddressRepository : ICustomerSupplierAddressRepository
    {
        public CustomerSupplierAddressRepository() 
        {
         
        }
        public async Task CreateAsync(CustomerSupplierAddress customerSupplierAddress, ChallengerSNSYSDbContext dbContext)
        {
            dbContext.CustomerSupplierAddress.Add(customerSupplierAddress);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id, ChallengerSNSYSDbContext dbContext)
        {

            var customerSupplierAddress = await dbContext.CustomerSupplierAddress.FindAsync(id);

            dbContext.CustomerSupplierAddress.Remove(customerSupplierAddress);
            await dbContext.SaveChangesAsync();

        }

        public async Task<CustomerSupplierAddress> GetByIdAsync(int id, ChallengerSNSYSDbContext dbContext)
        {
            return await dbContext.CustomerSupplierAddress.FindAsync(id);
        }

        public async Task UpdateAsync(CustomerSupplierAddress customerSupplierAddress, ChallengerSNSYSDbContext dbContext)
        {
            dbContext.CustomerSupplierAddress.Update(customerSupplierAddress);
            await dbContext.SaveChangesAsync();

        }
    }
}
