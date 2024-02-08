using Microsoft.AspNetCore.Mvc;
using SNSYS.Challenger.Domain.Services.Interfaces;
using SNSYS.Challenger.InfraStructure.Interfaces;
using SNSYS.Challenger.InfraStructure.Model;

namespace SNSYS.Challenger.Api.Controllers
{
    [ApiController]
    [Route("snsys/api/auth")]
    public class AuthorizationController : Controller
    {
        public IAuthorizationJWT _authorizationJWT;
        public IUsersService _usersService;
        private readonly IConfiguration _configuration;
        public AuthorizationController(IAuthorizationJWT authorizationJWT, IConfiguration configuration, IUsersService usersService)
        {
            _authorizationJWT = authorizationJWT ?? throw new ArgumentNullException(nameof(authorizationJWT));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _usersService = usersService ?? throw new ArgumentNullException(nameof(usersService));
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
           
            bool isValid = ValidateCredentials(model.Username, model.Password).Result;

            if (isValid)
            {
               
                string jwtSecret= _configuration.GetValue<string>("JwtSecret");
                double minuteExpirationToken = _configuration.GetValue<double>("MinuteExpirationToken");

                var token = _authorizationJWT.GenerateJwtToken(model.Username,jwtSecret,minuteExpirationToken);
                return Ok(new { token });
            }
            else
            {
                return Unauthorized();
            }
        }

        private async Task<bool> ValidateCredentials(string username, string password)
        {
            var ret = await _usersService.ExistUser(username, password);

            return ret;
        }
    }

}

   


   
