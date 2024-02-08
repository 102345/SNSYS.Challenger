
using SNSYS.Challenger.Domain.Entities;

namespace SNSYS.Challenger.Domain.Repositories.User
{
    public interface IUsersRepository
    {
        Task<Users> GetByIdAsync(int id);
        Task<Users> GetAsync( string userName, string Password );
        Task<IEnumerable<Users>> GetAllAsync();
        Task CreateAsync(Users user);
        Task UpdateAsync(Users user);
        Task DeleteAsync(int id);
    }
}
