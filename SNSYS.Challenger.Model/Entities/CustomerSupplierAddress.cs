namespace SNSYS.Challenger.Domain.Entities
{
    public class CustomerSupplierAddress
    {
        public int Id { get; set; }
        public string Address { get; set; } = string.Empty; // Ensure non-null
        public string City { get; set; } = string.Empty; // Ensure non-null
        public int ZIP { get; set; }
        public string Country { get; set; } = string.Empty; // Ensure non-null
        public int CustomerSupplierId { get; set; }
        public CustomerSupplier CustomerSupplier { get; set; }
    }
}
