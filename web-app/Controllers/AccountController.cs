

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Bizpack.Data;
using Bizpack.Service.Account;
using Bizpack.Service.ApiResponse;
using Bizpack.Views.Account;

namespace Bizpack.Controllers {

    [Route ("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase {

        private AppDbContext _dbContext;
        private IUserVerificationService _userVerificattionService;

        private IJwtService _jwtService;
        public AccountController (AppDbContext dbContext, IUserVerificationService userVerificattionService, IJwtService jwtService) {
            _dbContext = dbContext;
            _userVerificattionService = userVerificattionService;
            _jwtService = jwtService;

        }





        /// <summary>
        /// Use this api to get JWT token genrerated. User must save this token and send it in
        /// bearer header with every request.
        /// </summary>
        /// <param name="LoginReq"></param>
        /// <response code="200">User's JWT token</response>

        [HttpPost ("login")]
        [Produces ("application/json")]
        [ProducesResponseType (201)]
        [ServiceFilter (typeof (ValidationFilterAttribute))]

        public async Task<Object> Login (LoginRequestModel LoginReq) {

            AppUser user = await _userVerificattionService.CheckUser (LoginReq.Email, LoginReq.Password);
            if (user is null) return new ApiBadRequestResponse (400, new [] { "User does not exist" });

            string token = _jwtService.JwtCreator (
                new List<Claim> () {
                    new Claim ("RoleValue", "Admin")
                });

            LoginResponseViewModel loginResponse = new LoginResponseViewModel () {
                Email = user.Email,
                Token = token
            };

            return Ok (new ApiOkResponse (loginResponse));
        }





        /// <summary>
        /// Creates new user
        /// </summary>
        /// <param name="signupReq"></param>
        /// <returns></returns>

        [HttpPost ("signup")]
        public IActionResult SignUp (SignupRequestModel signupReq) {
            // TODO: add sign-up service
            return Ok (new { yo = "yo" });
        }

    }
}