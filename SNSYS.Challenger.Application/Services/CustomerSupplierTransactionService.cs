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

        public async Task DeleteAsync(int id)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                await _unitOfWork.CustomerSupplierContacts.DeletePerCustomerSupplier(id, _context);

                await _unitOfWork.CustomerSupplierAddresses.DeletePerCustomerSupplier(id, _context);

                await _unitOfWork.CustomerSuppliers.DeleteAsync(id, _context);

                _unitOfWork.Commit();

            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                _unitOfWork.RollBack();

            }
        }

        public async Task<IEnumerable<CustomerSupplierData>> GetAllAsync(FilterCustomerSupplier filterCustomerSupplier)
        {
            return await _unitOfWork.CustomerSuppliers.GetAllAsync(filterCustomerSupplier, _context);
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
