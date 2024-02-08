using Microsoft.EntityFrameworkCore;
using SNSYS.Challenger.Domain.Entities;
using SNSYS.Challenger.Domain.Repositories.User;
using SNSYS.Challenger.InfraStructure.Data.Context;

namespace SNSYS.Challenger.InfraStructure.Repositories.User
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ChallengerSNSYSDbContext _context;

        protected DbSet<Users> Table => _context.Users;
        public UsersRepository(ChallengerSNSYSDbContext challengerSNSYSDbContext) 
        { 
            
            _context = challengerSNSYSDbContext ?? throw new ArgumentNullException(nameof(challengerSNSYSDbContext));
        
        }


        public async Task CreateAsync(Users user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

        }

        public async Task<Users> GetAsync(string userName, string Password)
        {
            return await Table.Where(f => f.Name == userName && f.Password == Password)
                      .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Users>> GetAllAsync()
        {
            return await Table.ToListAsync();
        }

        public async Task<Users> GetByIdAsync(int id)
        {
            return await Table.FindAsync(id);  
        }

        public async Task UpdateAsync(Users user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
