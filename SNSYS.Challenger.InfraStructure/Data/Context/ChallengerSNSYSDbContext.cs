using Microsoft.EntityFrameworkCore;
using SNSYS.Challenger.Domain.Entities;

namespace SNSYS.Challenger.InfraStructure.Data.Context
{
    public class ChallengerSNSYSDbContext : DbContext
    {
        public DbSet<Users> Users { get; set; }

        public DbSet<CustomerSupplier> CustomerSupplier{ get; set; }

        public DbSet<CustomerSupplierContact> CustomerSupplierContact { get; set; }

        public DbSet<CustomerSupplierAddress> CustomerSupplierAddress { get; set; }

        public ChallengerSNSYSDbContext(DbContextOptions<ChallengerSNSYSDbContext> options) :
          base(options)
        {
           
        }

    }
}
