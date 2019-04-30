

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Bizpack.Data.Configuration;

namespace Bizpack.Service.Account {

    public interface IJwtValidatorService {
        IPrincipal ValidateToken (string authToken);
    }

    public class JwtValidatorService : IJwtValidatorService {

        private IAppConfiguration _appConfig;

        private AuthConfigurartion _authConfig;

        public JwtValidatorService(IAppConfiguration appConfig)
        {
            _appConfig = appConfig;
            _authConfig = _appConfig.getAuthConfig();
        }
        public static TokenValidationParameters GetValidationParameters (string securityKey) {

            return new TokenValidationParameters () {
                ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = "http://localhost:5000",
                    ValidAudience = "http://localhost:5000",
                    IssuerSigningKey = new SymmetricSecurityKey (Encoding.UTF8.GetBytes (securityKey))
            };
        }

        public IPrincipal ValidateToken (string authToken) {
            var tokenHandler = new JwtSecurityTokenHandler ();
            var validationParameters = JwtValidatorService.GetValidationParameters (_authConfig.SecurityKey);

            SecurityToken validatedToken;
            try {
                IPrincipal principal = tokenHandler.ValidateToken (authToken, validationParameters, out validatedToken);
                return principal;
            } catch (Exception ex) {
                // token couldn't be validated
                // TODO: Add log if needed.
                return null;
            }

        }

    }
}