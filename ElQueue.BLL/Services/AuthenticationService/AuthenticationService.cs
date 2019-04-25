using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Dawn;
using ElQueue.BLL.Models;
using ElQueue.DAL.Models;
using ElQueue.DAL.UnitOfWork;
using Microsoft.AspNetCore.Identity;

namespace ElQueue.BLL.Services.AuthenticationService
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork _storage;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public AuthenticationService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager)
        {
            _storage = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<UserBm> SignInAsync(string login, string password)
        {
            var user = (await _storage.Users.GetAsync(u => u.UserName == login)).SingleOrDefault();
            Guard.Argument(() => user).NotNull();

            var validationResult = _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, password);

            var isValid = validationResult == PasswordVerificationResult.Success;
            Guard.Argument(() => isValid).True();

            return _mapper.Map<UserBm>(user);
        }

        public async Task<UserBm> RegisterAsync(RegisterModel registerUser)
        {
            Guard.Argument(() => registerUser).NotNull();

            var confirmation = registerUser.Password == registerUser.ConfirmPassword;
            Guard.Argument(() => confirmation).True();

            var user = _mapper.Map<User>(registerUser);
            var identityResult = await _userManager.CreateAsync(user, registerUser.Password);

            //identityResult.Succeeded

            await _storage.SaveAsync();

            return _mapper.Map<UserBm>(user);
        }

        public async Task<UserBm> GetUserByName(string userName)
        {
            Guard.Argument(() => userName).NotEmpty();

            var user = (await _storage.Users.GetAsync(u => u.UserName == userName)).FirstOrDefault();

            return _mapper.Map<UserBm>(user);
        }

        private ClaimsIdentity Authenticate(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, "jwt"); // (claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            return id;
        }
    }
}
