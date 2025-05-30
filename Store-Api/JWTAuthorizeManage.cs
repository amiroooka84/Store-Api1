﻿using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using StoreApi.Entity._User;
using StoreApi.Models;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StoreApi
{

    public class JWTAuthorizeManage
    {
        private readonly UserManager<User> _userManager;
        public JWTAuthorizeManage(UserManager<User> userManager) {
            _userManager = userManager;
        }
        public async Task<JWT_Fields> AuthenticateAsync(string? PhoneNumber)
        {


            var TokenExpireTimeStamp = DateTime.Now.AddDays(Constansts.JWT_TOKEN_EXPIRE_TIME);

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            var Tokenkey = Encoding.ASCII.GetBytes(Constansts.JWT_SECURITY_KEY_FOR_TOKEN);

            string RoleUser = "User";
            User user = new User();
            user = await _userManager.FindByNameAsync(PhoneNumber) ;
            var result = await _userManager.GetClaimsAsync(user);
            var isRole = result.First();
            if (isRole.Type == "AdminNumber")
            {
                RoleUser = "Admin";
            }


            var SecurityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new List<Claim>
                {
                    new Claim("PhoneNumber" , PhoneNumber),
                    new Claim(ClaimTypes.Role , RoleUser),
                    new Claim(isRole.Type , isRole.Value),
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
                Expire_Time = TokenExpireTimeStamp,
                Role = result.FirstOrDefault().Type == "UserNumber" ? "3" : _userManager.GetClaimsAsync(user).Result.FirstOrDefault().Value,
            };
        }

    }
}
