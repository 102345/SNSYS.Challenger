namespace SNSYS.Challenger.Api.Contracts
{
    public class CustomerSupplierAddressRequest
    {
        public string Address { get; set; } = string.Empty; // Ensure non-null
        public string City { get; set; } = string.Empty; // Ensure non-null
        public string ZIP { get; set; }
        public string Country { get; set; } = string.Empty; // Ensure non-null
    }
}
