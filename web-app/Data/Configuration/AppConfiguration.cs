

using System;
using Microsoft.Extensions.Configuration;

namespace Bizpack.Data.Configuration {

    public interface IAppConfiguration
    {
        AuthConfigurartion getAuthConfig();
    }


    public class AppConfiguration: IAppConfiguration {

        private IConfiguration _config;
        private AuthConfigurartion AuthConfiguration = new AuthConfigurartion ();

        public AppConfiguration (IConfiguration config) {
            _config = config;
            _config.GetSection (nameof (AuthConfiguration)).Bind (AuthConfiguration);
        }

        public AuthConfigurartion getAuthConfig() {
            return AuthConfiguration;
        }

    }
}