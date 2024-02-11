using Microsoft.EntityFrameworkCore;
using SNSYS.Challenger.Application.Contracts;
using SNSYS.Challenger.Application.Services.Interfaces;
using SNSYS.Challenger.Domain.Entities;
using SNSYS.Challenger.Domain.Filter;
using SNSYS.Challenger.InfraStructure.Data.Context;

namespace SNSYS.Challenger.Application.Services
{
    public class CustomerSupplierTransactionService : ICustomerSupplierTransactionService
    {
        private readonly ChallengerSNSYSDbContext _context;
        private UnitOfWork _unitOfWork;

        public CustomerSupplierTransactionService(ChallengerSNSYSDbContext challengerSNSYSDbContext)
        {
            _context = challengerSNSYSDbContext ?? throw new ArgumentNullException(nameof(challengerSNSYSDbContext));
            _context.ChangeTracker.AutoDetectChangesEnabled = false;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            _unitOfWork = new UnitOfWork(_context);

        }


        //public async Task CreateAsync1(CustomerSupplierModel customerSupplierModel)
        //{
        //    //var unitOfWork = new UnitOfWork(_context);
        //    //using (var unitOfWork = new UnitOfWork(_context))
        //    var ChallengerDb = "Host=localhost;Port=5432;Pooling=true;Database=ChallengerSNYS;User Id=postgres;Password=scnfm;";

        //    var options = new DbContextOptionsBuilder<ChallengerSNSYSDbContext>()
        //                                            .UseNpgsql(ChallengerDb)
        //                                            .Options;

        //    using var context1 = new ChallengerSNSYSDbContext(options);

        //    var transaction = await context1.Database.BeginTransactionAsync();
        //    try
        //    {
        //        //await unitOfWork.BeginTransaction();



        //        var customerSupplier = new CustomerSupplier()
        //        {
        //            Name = customerSupplierModel.Name,
        //            DocumentNumber = customerSupplierModel.DocumentNumber,
        //            Type = customerSupplierModel.Type,
        //        };

        //        //var customerSupplierId = await unitOfWork.CustomerSuppliers.CreateAsync(customerSupplier,_context);

        //        var customerSupplierId = await context1.CustomerSupplier.AddAsync(customerSupplier);

        //        await context1.SaveChangesAsync();



        //        await transaction.CommitAsync();

        //        //foreach (var addressItem in customerSupplierOrchestrator.Addresses)
        //        //{
        //        //    addressItem.CustomerSupplierId = customerSupplier.Id;
        //        //    await unitOfWork.CustomerSupplierAddresses.CreateAsync(addressItem, _context);
        //        //}

        //        //foreach (var contactItem in customerSupplierOrchestrator.Contacts)
        //        //{
        //        //    contactItem.CustomerSupplierId = customerSupplier.Id;
        //        //    await unitOfWork.CustomerSupplierContacts.CreateAsync(contactItem, _context);
        //        //}

        //    }
        //    catch (Exception ex)
        //    {
             
        //        await transaction.RollbackAsync();
        //    }


        //}


        public async Task CreateAsync(CustomerSupplierModel customerSupplierModel)
        {
           
         
            try
            {
                await _unitOfWork.BeginTransaction();

                var customerSupplier = new CustomerSupplier()
                {
                    Name = customerSupplierModel.Name,
                    DocumentNumber = customerSupplierModel.DocumentNumber,
                    Type = customerSupplierModel.Type,
                };

                var customerSupplierId = await _unitOfWork.CustomerSuppliers.CreateAsync(customerSupplier,_context);


                foreach (var addressItem in customerSupplierModel.Addresses)
                {
                    addressItem.CustomerSupplierId = customerSupplierId;
                    await _unitOfWork.CustomerSupplierAddresses.CreateAsync(addressItem, _context);
                }

                foreach (var contactItem in customerSupplierModel.Contacts)
                {
                    contactItem.CustomerSupplierId = customerSupplierId;
                    await _unitOfWork.CustomerSupplierContacts.CreateAsync(contactItem, _context);
                }

                _unitOfWork.Commit();

            }
            catch (Exception ex)
            {   
                string msg = ex.Message;
                _unitOfWork.RollBack();
                
            }


        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CustomerSupplierData>> GetAllAsync(FilterCustomerSupplier filterCustomerSupplier)
        {
            throw new NotImplementedException();
        }

        public async Task<CustomerSupplier> GetByIdAsync(int id)
        {
            return await _unitOfWork.CustomerSuppliers.GetByIdAsync(id,_context);
        }

        public async Task UpdateAsync(CustomerSupplierModel customerSupplierModel)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var customerSupplier = new CustomerSupplier()
                {   
                    Id = customerSupplierModel.Id.Value,
                    Name = customerSupplierModel.Name,
                    DocumentNumber = customerSupplierModel.DocumentNumber,
                    Type = customerSupplierModel.Type,
                };

                 await _unitOfWork.CustomerSuppliers.UpdateAsync(customerSupplier, _context);


                foreach (var addressItem in customerSupplierModel.Addresses)
                {
                    await _unitOfWork.CustomerSupplierAddresses.UpdateAsync(addressItem, _context);
                }

                foreach (var contactItem in customerSupplierModel.Contacts)
                {
                   
                    await _unitOfWork.CustomerSupplierContacts.UpdateAsync(contactItem, _context);
                }

                _unitOfWork.Commit();

            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                _unitOfWork.RollBack();

            }

        }
    }
}
