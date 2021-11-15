using EmailService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using SportDataAuth.DbContext;
using SportDataAuth.DbContext.Models;
using SportDataAuth.Responses;
using SportDataAuth.Shared.AuthRequests;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenService;

namespace SportDataAuth.Services
{
    public class MyAuthenticationService : IMyAuthenticationService
    {
        private readonly AuthDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly UserValidator _userValidator;
        private readonly RefreshTokenValidator _refreshTokenValidator;

        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailService;
        private readonly IRefreshTokenService _refreshTokenService;

        private const string RegularUserRole = "RegularUser";

        public MyAuthenticationService(AuthDbContext db, UserManager<IdentityUser> userManager, UserValidator userValidator, 
            RefreshTokenValidator refreshTokenValidator, IConfiguration configuration, IEmailSender emailService, 
            IRefreshTokenService refreshTokenService)
        {
            _db = db;
            _userManager = userManager;
            _userValidator = userValidator;
            _refreshTokenValidator = refreshTokenValidator;
            _configuration = configuration;
            _emailService = emailService;
            _refreshTokenService = refreshTokenService;
        }



        #region SignIn

        // Rgister
        public async Task<UserManagerResponse> RegisterUserAsync(RegisterRequest register)
        {
            if (register is null) throw new NullReferenceException("Register is null");

            if (register.Password != register.ConfirmPassword)
                return new UserManagerResponse
                {
                    Message = "Passwords doesn't match.",
                    IsSucess = false,
                };

            var identityUser = new IdentityUser
            {
                Email = register.Email,
                UserName = register.UserName,
                PhoneNumber = register.PhoneNumber
            };

            var result = await _userManager.CreateAsync(identityUser, register.Password);

            if (result.Succeeded)
            {
                // roles
                var user = await _userManager.FindByEmailAsync(identityUser.Email);
                await _userManager.AddToRoleAsync(user, RegularUserRole);

                // token
                var confirmEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(identityUser);
                var encodeEmailToken = Encoding.UTF8.GetBytes(confirmEmailToken);
                var validEmailToken = WebEncoders.Base64UrlEncode(encodeEmailToken);

                string url = $"{_configuration["Url"]}api/signIn/confirmemail?userid={identityUser.Id}&token={validEmailToken}";

                var message = new Message(new string[] { identityUser.Email }, "Confirm your Email", "<h1>Welcome<h/1>" +
                    $"<p>Please confirm your email by <a href='{url}'>clicking here</a></p>");
                await _emailService.SendEmailAsync(message);

                return new UserManagerResponse
                {
                    Message = "User created succesfully! Waiting email confirmation, it is necessary to login",
                    IsSucess = true,
                };
            }

            return new UserManagerResponse
            {
                Message = "User did not create",
                IsSucess = false,
                Errors = result.Errors.Select(x => x.Description),
            };

        }

        // Confirm Email
        public async Task<UserManagerResponse> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                return new UserManagerResponse
                {
                    Message = "User not found",
                    IsSucess = false
                };

            var decodedToken = WebEncoders.Base64UrlDecode(token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManager.ConfirmEmailAsync(user, normalToken);

            if (result.Succeeded)
                return new UserManagerResponse
                {
                    Message = "Email confirmed successfully!",
                    IsSucess = true
                };

            return new UserManagerResponse
            {
                Message = "Email did not confirm",
                IsSucess = false,
                Errors = result.Errors.Select(x => x.Description)
            };
        }

        // Confirm Phone Number
        public Task<UserManagerResponse> ConfirmPhoneAsync()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region LogIn

        // LogIn
        public async Task<UserManagerResponse> LoginUserAsync(LoginRequest login)
        {
            if (login is null) throw new NullReferenceException("Login is null");

            var user = await _userManager.FindByEmailAsync(login.Email);
            var userRoles = await _userManager.GetRolesAsync(user);

            if (user is null)
                return new UserManagerResponse
                {
                    Message = "There is no user with that Email",
                    IsSucess = false,
                };

            var result = await _userManager.CheckPasswordAsync(user, login.Password);

            if (result is false)
                return new UserManagerResponse
                {
                    Message = "Invalid Password",
                    IsSucess = false,
                };

            return await _userValidator.Authenticate(user, userRoles);
        }

        // LogOut
        public async Task<UserManagerResponse> LogOutAsync(string userId)
        {

            if (userId == null)
                return new UserManagerResponse
                {
                    Message = "User id doesn´t exist",
                    IsSucess = false,
                };

            await _refreshTokenService.DeleteAllAsync(userId);

            return new UserManagerResponse
            {
                Message = "Logout Succesfully",
                IsSucess = true
            };
        }

        // Refresh
        public async Task<UserManagerResponse> RefreshTokenAsync(string refreshTokenShared)
        {
            if (refreshTokenShared is null) throw new NullReferenceException("Refresh Token is null");

            bool isValidRefreshToken = _refreshTokenValidator.ValidateRefreshToken(refreshTokenShared);

            if (!isValidRefreshToken)
                return new UserManagerResponse
                {
                    Message = "Refresh token is not valid",
                    IsSucess = false,
                };

            RefreshToken RefreshToken = await _refreshTokenService.GetByTokenAsync(refreshTokenShared);

            if (RefreshToken == null)
                return new UserManagerResponse
                {
                    Message = "Refresh token is not valid",
                    IsSucess = false,
                };

            await _refreshTokenService.DeleteAsync(RefreshToken.Id);

            var user = await _userManager.FindByIdAsync(RefreshToken.UserId);

            if (user == null)
                return new UserManagerResponse
                {
                    Message = "User not found (refreshtoken/UserService)",
                    IsSucess = false,
                };

            var userRoles = await _userManager.GetRolesAsync(user);

            return await _userValidator.Authenticate(user, userRoles);

        }

        #endregion



    }
}
