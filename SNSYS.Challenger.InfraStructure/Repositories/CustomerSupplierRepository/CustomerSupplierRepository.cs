using Microsoft.EntityFrameworkCore;
using SNSYS.Challenger.Domain.Entities;
using SNSYS.Challenger.Domain.Filter;
using SNSYS.Challenger.InfraStructure.Data.Context;
using SNSYS.Challenger.InfraStructure.Interfaces.Repositories;

namespace SNSYS.Challenger.InfraStructure.Repositories.CustomerSupplierRepository
{
    public class CustomerSupplierRepository : ICustomerSupplierRepository
    {

        public CustomerSupplierRepository()
        {

        }


        public async Task<int> CreateAsync(CustomerSupplier customerSupplier, ChallengerSNSYSDbContext dbContext)
        {
            int ret = 0;

            try
            {
                var entity = await dbContext.CustomerSupplier.AddAsync(customerSupplier);
                await dbContext.SaveChangesAsync();

                ret = entity.Entity.Id;


            }
            catch (Exception ex)
            {
                string msg = ex.Message;
             
            }
            
            return ret;

        }

        public async Task DeleteAsync(int id, ChallengerSNSYSDbContext dbContext)
        {

            var customerSupplier = await dbContext.CustomerSupplier.FindAsync(id);

            dbContext.CustomerSupplier.Remove(customerSupplier);
            await dbContext.SaveChangesAsync();


        }


        public async Task<IEnumerable<CustomerSupplierData>> GetAllAsync(FilterCustomerSupplier filterCustomerSupplier, 
                                    ChallengerSNSYSDbContext dbContext)
        {

            var query = from cs in dbContext.CustomerSupplier
                        join csa in dbContext.CustomerSupplierAddress on cs.Id equals csa.CustomerSupplierId into csaGroup
                        from csa in csaGroup.DefaultIfEmpty()
                        join csc in dbContext.CustomerSupplierContact on cs.Id equals csc.CustomerSupplierId into cscGroup
                        from csc in cscGroup.DefaultIfEmpty()
                        select new CustomerSupplierData()
                        {
                            Id = cs.Id,
                            Name = cs.Name,
                            Type = cs.Type,
                            DocumentNumber = cs.DocumentNumber,
                            Address = csa.Address == null ? string.Empty : csa.Address,
                            City = csa.City == null ? string.Empty : csa.City,
                            ZIP = csa.ZIP == null ? string.Empty : csa.ZIP,
                            Country = csa.Country == null ? string.Empty : csa.Country,
                            ContactName = csc.Name == null ? string.Empty : csc.Name,
                            ContactEmail = csc.Email == null ? string.Empty : csc.Email,
                            ContactPhoneNumber = csc.PhoneNumber == null ? 0 : csc.PhoneNumber,
                            ContactPosition = csc.Position == null ? string.Empty : csc.Position
                        };


            if (!string.IsNullOrEmpty(filterCustomerSupplier.name))
            {
                var nameCompany = string.Format("%{0}%", filterCustomerSupplier.name.ToUpper());

                query = query.Where(cs => EF.Functions.Like(cs.Name.ToUpper(), nameCompany));
            }

            if (!string.IsNullOrEmpty(filterCustomerSupplier.documentNumber))
            {
                var documentNumber = string.Format("%{0}%", filterCustomerSupplier.documentNumber);

                query = query.Where(cs => EF.Functions.Like(cs.DocumentNumber, documentNumber));
            }

            if (!string.IsNullOrEmpty(filterCustomerSupplier.city))
            {
                var city = string.Format("%{0}%", filterCustomerSupplier.city);

                query = query.Where(csa => EF.Functions.Like(csa.City, city));
            }

            if (!string.IsNullOrEmpty(filterCustomerSupplier.country))
            {
                var country = string.Format("%{0}%", filterCustomerSupplier.country);

                query = query.Where(csa => EF.Functions.Like(csa.Country, country));
            }

            if (!string.IsNullOrEmpty(filterCustomerSupplier.contactName))
            {
                var contactName = string.Format("%{0}%", filterCustomerSupplier.contactName);

                query = query.Where(csc => EF.Functions.Like(csc.ContactName, contactName));
            }

            if (!string.IsNullOrEmpty(filterCustomerSupplier.contactPosition))
            {
                var contactPosition = string.Format("%{0}%", filterCustomerSupplier.contactPosition);

                query = query.Where(csc => EF.Functions.Like(csc.ContactPosition, contactPosition));
            }

            query = query.Skip((filterCustomerSupplier.pageIndex - 1) * filterCustomerSupplier.pageSize).Take(filterCustomerSupplier.pageSize);

            return await query.ToListAsync();
        }

        public async Task<CustomerSupplier> GetByIdAsync(int id, ChallengerSNSYSDbContext dbContext)
        {
            return await dbContext.CustomerSupplier.FindAsync(id);
        }

        public async Task UpdateAsync(CustomerSupplier customerSupplier, ChallengerSNSYSDbContext dbContext)
        {
            try
            {

                dbContext.CustomerSupplier.Update(customerSupplier);

                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                string message = ex.Message;
            }

        }

    }
}
