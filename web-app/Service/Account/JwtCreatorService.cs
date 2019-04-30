
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Bizpack.Data.Configuration;

namespace Bizpack.Service.Account 
{

    public interface IJwtService {
        string JwtCreator(List<Claim> Claims);
    }

    public class JwtService : IJwtService
    {
        private IAppConfiguration _appConfig;

        private AuthConfigurartion _authConfig;

        public JwtService(IAppConfiguration appConfig)
        {
            _appConfig = appConfig;
            _authConfig = _appConfig.getAuthConfig();
        }
        public string JwtCreator(List<Claim> Claims) 
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authConfig.SecurityKey));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
 
            var tokeOptions = new JwtSecurityToken(
                issuer: _authConfig.ValidIssuer,
                audience: _authConfig.ValidAudience,
                claims: Claims,
                expires: DateTime.Now.AddMinutes(_authConfig.ExpirationTimeInMinutes),
                signingCredentials: signinCredentials
            );
 
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return tokenString;
        }
    }
}
