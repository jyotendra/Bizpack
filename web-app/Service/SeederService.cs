

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Bizpack.Data;
using Bizpack.Service.Account;

namespace Bizpack.Service {

    public interface ISeederService {
        Task SeedUsers ();

        Task SeedRoles ();
    }

    public class SeederService : ISeederService {

        private AppDbContext _context;

        private UserManager<AppUser> _userManager;

        private RoleManager<IdentityRole> _roleManager;

        public SeederService (AppDbContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager) {
            this._context = context;
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        public async Task SeedRoles () {
            // apply all migrations if they haven't been yet
            _context.Database.Migrate ();

            // Seed admin role
            // Find if Admin role has been seeded already
            IdentityRole adminRole = await _context.Roles.FirstOrDefaultAsync (r => r.NormalizedName == "ADMIN");
            if (adminRole == null) {
                await _roleManager.CreateAsync (new IdentityRole {
                    Name = "admin"
                });
            }

        }

        public async Task SeedUsers () {
            // // apply all migrations if they haven't been yet
            _context.Database.Migrate ();

            // Seed Admin user
            // Find if admin has been seeded already
            AppUser adminUser = await _context.Users.FirstOrDefaultAsync (u => u.Email == "admin@admin.com");
            if (adminUser == null) {
                string passwordSalt = Salt.Create ();
                AppUser newAdminUser = new AppUser {
                    UserName = "admin@admin.com",
                    Email = "admin@admin.com",
                    PasswordSalt = passwordSalt,
                    PasswordHash = Hash.Create ("test123", passwordSalt)
                };

                IdentityResult result = await _userManager.CreateAsync (newAdminUser);
                // TODO: Apply check to see if the "admin" role even exists 
                if (result.Succeeded) { 
                    await _userManager.AddToRoleAsync (newAdminUser, "admin");
                }

            }

        }

        // private List<AppUser> CreateUsers(List<>) {

        // }
    }

}