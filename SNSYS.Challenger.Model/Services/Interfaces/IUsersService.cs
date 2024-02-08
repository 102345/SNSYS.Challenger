namespace SNSYS.Challenger.Domain.Services.Interfaces
{
    public interface IUsersService
    {
        Task<bool> ExistUser(string userName, string password);
    }
}
