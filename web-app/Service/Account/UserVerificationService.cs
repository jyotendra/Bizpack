

using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Bizpack.Data;

namespace Bizpack.Service.Account {

    public interface IUserVerificationService 
    {
        Task<AppUser> CheckUser(string email, string password);
    }

    public class UserVerificationService : IUserVerificationService
    {

        private AppDbContext _dbContext;
        private UserManager<AppUser> _userManager;
        public UserVerificationService (AppDbContext dbContext, UserManager<AppUser> userManager) {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<AppUser> CheckUser (string email, string password) {
            AppUser user = null;
            // check if user exists in db
            user = await _userManager.FindByEmailAsync (email);
            if (user is null) { return null; }
            // check if correct password is provided
            bool isPasswordCorrect = Hash.Validate(password, user.PasswordSalt, user.PasswordHash);

            if (!isPasswordCorrect) return null;
            return user;
        }
    }

}