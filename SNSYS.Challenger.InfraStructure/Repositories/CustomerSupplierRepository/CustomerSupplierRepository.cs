using Microsoft.EntityFrameworkCore;
using SNSYS.Challenger.Domain.Entities;
using SNSYS.Challenger.Domain.Filter;
using SNSYS.Challenger.Domain.Repositories.CustomerSupplierRepository;
using SNSYS.Challenger.InfraStructure.Data.Context;

namespace SNSYS.Challenger.InfraStructure.Repositories.CustomerSupplierRepository
{
    public class CustomerSupplierRepository : ICustomerSupplierRepository
    {
        private readonly ChallengerSNSYSDbContext _context;

        protected DbSet<CustomerSupplier> Table => _context.CustomerSupplier;

        public CustomerSupplierRepository(ChallengerSNSYSDbContext challengerSNSYSDbContext)
        {
            _context = challengerSNSYSDbContext ?? throw new ArgumentNullException(nameof(challengerSNSYSDbContext));
            _context.ChangeTracker.AutoDetectChangesEnabled = false;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }


        public async Task CreateAsync(CustomerSupplier customerSupplier)
        {

            _context.CustomerSupplier.Add(customerSupplier);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteAsync(int id)
        {

            var customerSupplier = await _context.CustomerSupplier.FindAsync(id);

            _context.CustomerSupplier.Remove(customerSupplier);
            await _context.SaveChangesAsync();


        }

        public async Task<IEnumerable<CustomerSupplier>> GetAllAsync()
        {
            return await Table.OrderBy(o => o.Name).ToListAsync();
        }

        public async Task<IEnumerable<CustomerSupplierData>> GetAllAsync(FilterCustomerSupplier filterCustomerSupplier)
        {

            var query = from cs in _context.CustomerSupplier
                        join csa in _context.CustomerSupplierAddress on cs.Id equals csa.CustomerSupplierId into csaGroup
                        from csa in csaGroup.DefaultIfEmpty()
                        join csc in _context.CustomerSupplierContact on cs.Id equals csc.CustomerSupplierId into cscGroup
                        from csc in cscGroup.DefaultIfEmpty()
                        select new CustomerSupplierData()
                        {
                            Id = cs.Id,
                            Name = cs.Name,
                            Type = cs.Type,
                            DocumentNumber = cs.DocumentNumber,
                            Address = csa.Address == null ? string.Empty : csa.Address,
                            City = csa.City == null ? string.Empty : csa.City,
                            ZIP = csa.ZIP == null ? 0 : csa.ZIP,
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


            return await query.ToListAsync();
        }

        public async Task<CustomerSupplier> GetByIdAsync(int id)
        {
            return await Table.FindAsync(id);
        }

        public async Task UpdateAsync(CustomerSupplier customerSupplier)
        {
            try
            {

                _context.CustomerSupplier.Update(customerSupplier);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                string message = ex.Message;
            }

        }
    }
}
