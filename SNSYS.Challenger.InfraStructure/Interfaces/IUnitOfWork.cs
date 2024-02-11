using SNSYS.Challenger.InfraStructure.Interfaces.Repositories;

namespace SNSYS.Challenger.InfraStructure.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerSupplierRepository CustomerSuppliers { get; }
        ICustomerSupplierAddressRepository CustomerSupplierAddresses { get; }

        ICustomerSupplierContactRepository CustomerSupplierContacts { get; }
        Task BeginTransaction();
        
        Task Commit();

        Task RollBack();

    }
}
