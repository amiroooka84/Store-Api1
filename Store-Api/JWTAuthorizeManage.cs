using Microsoft.IdentityModel.Tokens;
using StoreApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StoreApi
{

    public class JWTAuthorizeManage
    {
        public JWT_Fields Authenticate(string? PhoneNumber)
        {


            var TokenExpireTimeStamp = DateTime.Now.AddDays(Constansts.JWT_TOKEN_EXPIRE_TIME);

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            var Tokenkey = Encoding.ASCII.GetBytes(Constansts.JWT_SECURITY_KEY_FOR_TOKEN);

            var SecurityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new List<Claim>
                {
                    new Claim("PhoneNumber" , PhoneNumber),
                    new Claim(ClaimTypes.PrimaryGroupSid , "User Group 01")
                }),
                Expires = TokenExpireTimeStamp,

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Tokenkey),SecurityAlgorithms.Aes192CbcHmacSha384)
            };

            var SecurityToken = jwtSecurityTokenHandler.CreateToken(SecurityTokenDescriptor);
            var Token = jwtSecurityTokenHandler.WriteToken(SecurityToken);

            return new JWT_Fields
            {
                Token = Token,
                PhoneNumber = PhoneNumber,
                Expire_Time = TokenExpireTimeStamp
            };
        }


    }
}
