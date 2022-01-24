﻿using Coyote.Models.User;
using Coyote.Models.Authentication;
using Coyote.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Puma.Prey.Rabbit.Models;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Coyote.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<PumaUser> _userManager;

        public UserService(UserManager<PumaUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> CreateUser(UserRequest model)
        {
            var pumaUser = new PumaUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = model.Email,
                UserName = model.Email,
                PhoneNumber = model.PhoneNumber,
                CreditCardNumber = model.CreditCardNumber,
                CreditCardExpiration = model.CreditCardExpiration,
                BillingAddress1 = model.BillingAddress1,
                BillingAddress2 = model.BillingAddress2,
                BillingCity = model.BillingCity,
                BillingState = model.BillingState,
                BillingZip = model.BillingZip
            };

            return await _userManager.CreateAsync(pumaUser, model.Password);
        }

        public async Task<bool> DoesUserExist(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user != null;
        }

        public async Task<User> ShowUsers(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            var user1 = new User
            {
                Email = user.Email,
                CreditCardNumber = user.CreditCardNumber,
                CreditCardExpiration = user.CreditCardExpiration,
                PhoneNumber = user.PhoneNumber,
                BillingAddress1 = user.BillingAddress1,
                BillingAddress2 = user.BillingAddress2,
                BillingCity = user.BillingCity,
                BillingState = user.BillingState,
                BillingZip = user.BillingZip,
            };
            return user1;
        }

        public async Task<IdentityResult> UpdateUser(UserUpdate model)
        {
            var users = await _userManager.FindByEmailAsync(model.Email);

            users.PhoneNumber = model.PhoneNumber;
            users.CreditCardNumber = model.CreditCardNumber;
            users.CreditCardExpiration = model.CreditCardExpiration;
            users.BillingAddress1 = model.BillingAddress1;
            users.BillingAddress2 = model.BillingAddress2;
            users.BillingCity = model.BillingCity;
            users.BillingState = model.BillingState;
            users.BillingZip = model.BillingZip;

            var result = await _userManager.UpdateAsync(users);
            return result;
        }

        public async Task<IdentityResult> ChangePassword(ChangePasswordRequest request, PumaUser user)
        {
            return await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
        }

        public async Task<PumaUser> GetUserAsync(ClaimsPrincipal principal)
        {
            return await _userManager.GetUserAsync(principal);
        }
    }
}