using SNSYS.Challenger.Domain.Entities;
using SNSYS.Challenger.InfraStructure.Interfaces.Repositories;
using SNSYS.Challenger.InfraStructure.Data.Context;

namespace SNSYS.Challenger.InfraStructure.Repositories.CustomerSupplierContactRepository
{
    public class CustomerSupplierContactRepository : ICustomerSupplierContactRepository
    {


        public CustomerSupplierContactRepository()
        {

        }

        public async Task CreateAsync(CustomerSupplierContact customerSupplierContact, ChallengerSNSYSDbContext dbContext)
        {
            dbContext.CustomerSupplierContact.Add(customerSupplierContact);
            await dbContext.SaveChangesAsync();

        }

        public async Task DeleteAsync(int id, ChallengerSNSYSDbContext dbContext)
        {
            var customerSupplierContact = await dbContext.CustomerSupplierContact.FindAsync(id);

            dbContext.CustomerSupplierContact.Remove(customerSupplierContact);
            await dbContext.SaveChangesAsync();

        }

        public async Task DeletePerCustomerSupplier(int customerSupplierId, ChallengerSNSYSDbContext dbContext)
        {
            var customerSupplierContacts = dbContext.CustomerSupplierContact.Where(x => x.CustomerSupplierId == customerSupplierId).ToList();

            dbContext.CustomerSupplierContact.RemoveRange(customerSupplierContacts);

            await dbContext.SaveChangesAsync();
        }

        public async Task<CustomerSupplierContact> GetByIdAsync(int id, ChallengerSNSYSDbContext dbContext)
        {
            return await dbContext.CustomerSupplierContact.FindAsync(id);
        }

        public async Task UpdateAsync(CustomerSupplierContact customerSupplierContact, ChallengerSNSYSDbContext dbContext)
        {
            dbContext.CustomerSupplierContact.Update(customerSupplierContact);
            await dbContext.SaveChangesAsync();

        }
    }
}
