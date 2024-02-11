using SNSYS.Challenger.Domain.Entities;

namespace SNSYS.Challenger.Api.Contracts
{   
    public class CustomerSupplierResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public char Type { get; set; }
        public string DocumentNumber { get; set; }

        public CustomerSupplierAddress Addresses { get; set; }

        public CustomerSupplierContact Contacts { get; set; }


    }
}
