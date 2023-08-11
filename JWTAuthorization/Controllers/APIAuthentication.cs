using JWTAuthorization.Helper;
using JWTAuthorization.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTAuthorization.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class APIAuthentication : ControllerBase
    {

        [HttpGet]
        [Route("api/GenerateToken")]
        public async Task<ListReturnType<JWTAPIToken>> GenerateJWTToken(string UserId, string Password)
        {
            ListReturnType<JWTAPIToken> response = new ListReturnType<JWTAPIToken>();
            List<JWTAPIToken> Details = new List<JWTAPIToken>();
            if (UserId == "MSCommercialUser" && Password == "C:3gXw_82>4v4`Hs")
            {
                JWTAPIToken JWTT = JwtHelper.GenerateJWTTokens(UserId, Password);

                JWTAPIToken Typedetail = new JWTAPIToken();
                Typedetail.Access_Token = "Bearer " + JWTT.Access_Token;
                Details.Add(Typedetail);

                response.code = (int)ServiceMassageCode.SUCCESS; ;
                response.message = Convert.ToString(ServiceMassageCode.SUCCESS);
                response.result = Details;
            }
            else
            {
                response.code = (int)ServiceMassageCode.SUCCESS; ;
                response.message = Convert.ToString(ServiceMassageCode.UNAUTHORIZED_USER);
                response.result = null;
            }

            return response;
        }
    }
}
