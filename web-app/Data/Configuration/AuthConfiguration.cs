

namespace Bizpack.Data.Configuration {
    public class AuthConfigurartion {
        public bool ValidateAudience { get; set; }
        public bool ValidateLifetime { get; set; }
        public bool ValidateIssuerSigningKey { get; set; }
        public string SecurityKey { get; set; }
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
        public int ExpirationTimeInMinutes { get; set; }

    }
}
