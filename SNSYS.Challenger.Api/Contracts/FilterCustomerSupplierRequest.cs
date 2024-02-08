namespace SNSYS.Challenger.Api.Contracts
{
    public class FilterCustomerSupplierRequest : PaginatedRequest
    {
        public string? name { get; set; }

        public string? documentNumber { get; set; }

        public string? city { get; set; }

        public string? country { get; set; }

        public string? contactName { get; set; }

        public string? contactPosition { get; set; }

    }
}
