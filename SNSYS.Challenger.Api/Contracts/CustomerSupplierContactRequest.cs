namespace SNSYS.Challenger.Api.Contracts
{
    public class CustomerSupplierContactRequest
    {   
        public int? Id { get; set; }
        public string Name { get; set; } = string.Empty; // Ensure non-null
        public string Email { get; set; }
        public int? PhoneNumber { get; set; } // Nullable for optional phone number
        public string Position { get; set; } = string.Empty; // Ensure non-null
        public string Department { get; set; }
        public int? CustomerSupplierId { get; set; }


    }
}