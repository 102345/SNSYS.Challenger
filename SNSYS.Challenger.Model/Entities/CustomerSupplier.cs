using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SNSYS.Challenger.Domain.Entities
{
    public class CustomerSupplier
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public char Type { get; set; }
        public string DocumentNumber { get; set; }
        public ICollection<CustomerSupplierAddress>? Addresses { get; set; }
        public ICollection<CustomerSupplierContact>? Contacts { get; set; }
    }
}
