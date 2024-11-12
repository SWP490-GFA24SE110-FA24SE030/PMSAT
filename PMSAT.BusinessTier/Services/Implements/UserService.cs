using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PMSAT.BusinessTier.Constants;
using PMSAT.BusinessTier.Enums;
using PMSAT.BusinessTier.Payload.Login;
using PMSAT.BusinessTier.Payload.Users;
using PMSAT.BusinessTier.Payload;
using PMSAT.BusinessTier.Services.Interfaces;
using PMSAT.BusinessTier.Services;
using PMSAT.BusinessTier.Utils;
using PMSAT.DataTier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PMSAT.BusinessTier.Services.Implements
{
    public class UserService : IUserService
    {
        private readonly PmsatContext _context;

        public UserService(PmsatContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateNewUser(CreateNewUserRequest request)
        {
            User user = await _context.Users.SingleOrDefaultAsync(
                predicate: x => x.Username.Equals(request.UserName));
            if (user != null) throw new BadHttpRequestException(MessageConstant.User.UserExisted);

            user = new User()
            {
                Id = Guid.NewGuid(),
                Role = request.Role.GetDescriptionFromEnum(),
                Username = request.UserName,
                Email = request.Email,
                Password = PasswordUtil.HashPassword(request.Password),
                Status = UserStatus.Activate.GetDescriptionFromEnum()
            };

            await _context.Users.AddAsync(user);
            return user.Id;
        }


        public async Task<GetUserReponse> GetUserDetail(Guid userId)
        {
            if (userId == Guid.Empty) throw new BadHttpRequestException(MessageConstant.User.EmptyUserId);

            User user = await _context.Users.SingleOrDefaultAsync(
                predicate: x => x.Id.Equals(userId))
                ?? throw new BadHttpRequestException(MessageConstant.User.UserNotFoundMessage);

            return new GetUserReponse(user.Id, EnumUtil.ParseEnum<RoleEnum>(user.Role),
                user.Username, user.Email, EnumUtil.ParseEnum<UserStatus>(user.Status));

            //return _mapper.Map<GetUserReponse>(user);
        }

        public async Task<LoginResponse?> Login(LoginRequest request)
        {
            User user = await _context.Users.SingleOrDefaultAsync(
               predicate: x => x.Username.Equals(request.Username)
               );

            if (user == null || !PasswordUtil.VerifyHashedPassword(user.Password, request.Password))
                return null;

            return new LoginResponse()
            {
                UserId = user.Id,
                Username = user.Username,
                Role = EnumUtil.ParseEnum<RoleEnum>(user.Role),
                Status = EnumUtil.ParseEnum<UserStatus>(user.Status),
            };

        }

    }
}
