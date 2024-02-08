using Microsoft.IdentityModel.Tokens;
using SNSYS.Challenger.InfraStructure.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SNSYS.Challenger.InfraStructure.Authorization
{
    public class AuthorizationJWT : IAuthorizationJWT
    {
        public string GenerateJwtToken(string username, string jwtSecret, double minuteExpirationToken)
        {
            var key = Encoding.ASCII.GetBytes(jwtSecret);
            var claims = new[] { new Claim(ClaimTypes.NameIdentifier, username) };
            var token = new JwtSecurityToken(
                issuer: "SNSYS.Challenger.Api",
                audience: "SNSYS.Challenger.Api.Audience",
                claims: claims,
                expires: DateTime.Now.AddMinutes(minuteExpirationToken),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
