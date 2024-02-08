using SNSYS.Challenger.Domain.Repositories.User;
using SNSYS.Challenger.Domain.Services.Interfaces;

namespace SNSYS.Challenger.Domain.Services.User
{
    public class UsersService : IUsersService
    {
       
        public IUsersRepository _usersRepository;
        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository)); 
        }
        public async Task<bool> ExistUser(string userName, string password)
        {
            var user = await _usersRepository.GetAsync(userName, password);

            return user != null;
        }
    }
}
