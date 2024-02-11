using Microsoft.EntityFrameworkCore;

using SNSYS.Challenger.InfraStructure.Interfaces;
using SNSYS.Challenger.InfraStructure.Interfaces.Repositories;
using SNSYS.Challenger.InfraStructure.Repositories.CustomerSupplierAddressRepository;
using SNSYS.Challenger.InfraStructure.Repositories.CustomerSupplierContactRepository;
using SNSYS.Challenger.InfraStructure.Repositories.CustomerSupplierRepository;

namespace SNSYS.Challenger.InfraStructure.Data.Context
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ChallengerSNSYSDbContext _context;

        private ICustomerSupplierRepository _customerSupplierRepository;
        private ICustomerSupplierAddressRepository _customerSupplierAddressRepository;
        private ICustomerSupplierContactRepository _customerSupplierContactRepository;
        public UnitOfWork(ChallengerSNSYSDbContext challengerSNSYSDbContext) 
        {
            _context = challengerSNSYSDbContext ?? throw new ArgumentNullException(nameof(challengerSNSYSDbContext));
            _context.ChangeTracker.AutoDetectChangesEnabled = false;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
  

        }

        public ICustomerSupplierRepository CustomerSuppliers
        {
            get { return _customerSupplierRepository ?? (_customerSupplierRepository = new CustomerSupplierRepository()); }
        }

        public ICustomerSupplierAddressRepository CustomerSupplierAddresses
        {
            get { return _customerSupplierAddressRepository ?? (_customerSupplierAddressRepository = new CustomerSupplierAddressRepository()); }
        }

        public ICustomerSupplierContactRepository CustomerSupplierContacts
        {
            get { return _customerSupplierContactRepository ?? (_customerSupplierContactRepository = new CustomerSupplierContactRepository()); }
        }



        public async Task BeginTransaction()
        {   
            _context.Database.SetCommandTimeout(TimeSpan.FromSeconds(600));
            await _context.Database.BeginTransactionAsync();
        }

        public async Task Commit()
        {
            await _context.Database.CommitTransactionAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task RollBack()
        {
           await _context.Database.RollbackTransactionAsync();
        }
    }
}
