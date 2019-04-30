

using Microsoft.AspNetCore.Identity;

namespace Bizpack.Data 
{
    public class AppUser : IdentityUser 
    {
        public string PasswordSalt { get; set; }
    }
}