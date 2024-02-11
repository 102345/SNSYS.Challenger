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


        //public string Address { get; set; }
        //public string City { get; set; }
        //public string Country { get; set; }
        //public string Zip { get; set; }  
        //public string ContactName { get; set; }
        //public string ContactEmail { get; set; }
        //public int ContactPhoneNumber { get; set; }
        //public string ContactPosition { get; set; }


    }
}
