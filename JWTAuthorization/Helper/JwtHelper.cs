using JWTAuthorization.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace JWTAuthorization.Helper
{
    public class JwtHelper
    {
        public static JWTAPIToken GenerateJWTTokens(string UserId, string Password)
        {
            try
            {
                string jwtSecret = Convert.ToString("H@McQfTjWnZr4u7x!z % C * F - JaNdRgUkX");//Convert.ToString(ConfigurationManager.AppSettings["jwtSecret"]);
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.UTF8.GetBytes(jwtSecret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.NameIdentifier, UserId),
                new Claim(ClaimTypes.Name, Password)
            }),
                    Expires = DateTime.UtcNow.AddMinutes(30),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return new JWTAPIToken { Access_Token = tokenHandler.WriteToken(token) };
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static  ServiceHeaderInfo GetPrincipalFromExpiredToken(string token)
        {
            string jwtSecret = Convert.ToString("H@McQfTjWnZr4u7x!z % C * F - JaNdRgUkX");//Convert.ToString(ConfigurationManager.AppSettings["jwtSecret"]);
            ServiceHeaderInfo serviceHeader = new ServiceHeaderInfo();
            var Key = Encoding.UTF8.GetBytes(jwtSecret);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Key),
                ClockSkew = TimeSpan.Zero,
                RequireExpirationTime = true
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            JwtSecurityToken? jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                //throw new SecurityTokenException("Invalid token");
                serviceHeader.Message = "Invalid Token";
                serviceHeader.IsAuthenticated = false;
            }
            var expClaim = principal.Claims.First(x => x.Type == "exp").Value;
            var userid = principal.Claims.First(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
            var passwrd = principal.Claims.First(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;
            var tokenExpiryTime = Convert.ToInt64(expClaim);
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(tokenExpiryTime);
            if (dateTimeOffset < DateTime.UtcNow)
            {
                serviceHeader.Message = "Token Expired";
                serviceHeader.IsAuthenticated = false;
            }
            else
            {
                serviceHeader.Message = "Token Validated";
                serviceHeader.IsAuthenticated = true;
            }
            return serviceHeader;
        }

        public static  ServiceHeaderInfo AuthenticateJWTToken(string authorization)
        {
            ServiceHeaderInfo headerInfo = new ServiceHeaderInfo();
                      
            //If we don't find the authorization header return null
            if (string.IsNullOrEmpty(authorization))
            {
                return headerInfo;
            }
            //get the token from the auth header
            if (authorization.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                headerInfo.Token = authorization.Substring("Bearer ".Length).Trim();
            }
            return GetPrincipalFromExpiredToken(headerInfo.Token);
        }

    }
}
