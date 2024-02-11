namespace SNSYS.Challenger.Api.Contracts
{
    public class CustomerSupplierRequest
    {   
        public int? Id { get; set; }

        public string Name { get; set; }

        public char Type { get; set; }

        public string DocumentNumber { get; set; }

        public ICollection<CustomerSupplierAddressRequest>? Addresses { get; set; }
        public ICollection<CustomerSupplierContactRequest>? Contacts { get; set; }
    }
}
