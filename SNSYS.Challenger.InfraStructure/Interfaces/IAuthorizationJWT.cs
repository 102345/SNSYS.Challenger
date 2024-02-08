namespace SNSYS.Challenger.InfraStructure.Interfaces
{
    public interface IAuthorizationJWT
    {
        string GenerateJwtToken(string username, string jwtSecret, double minuteExpirationToken);
    }
}
