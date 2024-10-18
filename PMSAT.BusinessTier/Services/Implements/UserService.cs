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
using PMSAT.DataTier.Paginate;
using PMSAT.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSAT.BusinessTier.Services.Implements
{
    public class UserService : BaseService<UserService>, IUserService
    {
        public UserService(IUnitOfWork<PmsatContext> unitOfWork, ILogger<UserService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
        }

        public async Task<Guid> CreateNewUser(CreateNewUserRequest request)
        {
            User user = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(
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

            await _unitOfWork.GetRepository<User>().InsertAsync(user);
            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
            if (!isSuccessful) throw new BadHttpRequestException(MessageConstant.User.CreateUserFailed);
            return user.Id;
        }

        public Task<IPaginate<GetUserReponse>> GetListUser(UserFilter filter, PagingModel pagingModel)
        {
            var response = _unitOfWork.GetRepository<User>().GetPagingListAsync(
                selector: user => new GetUserReponse(user.Id, EnumUtil.ParseEnum<RoleEnum>(user.Role),
                user.Username, user.Email, EnumUtil.ParseEnum<UserStatus>(user.Status)),
                filter: filter,
                page: pagingModel.page,
                size: pagingModel.size,
                orderBy: x => x.OrderByDescending(x => x.Username)
                );

            return response;
        }

        public async Task<GetUserReponse> GetUserDetail(Guid userId)
        {
            if (userId == Guid.Empty) throw new BadHttpRequestException(MessageConstant.User.EmptyUserId);

            User user = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(
                predicate: x => x.Id.Equals(userId))
                ?? throw new BadHttpRequestException(MessageConstant.User.UserNotFoundMessage);

            //return new GetUserReponse(user.Id, EnumUtil.ParseEnum<RoleEnum>(user.Role),
            //    user.Username, user.Email, EnumUtil.ParseEnum<UserStatus>(user.Status));

            return _mapper.Map<GetUserReponse>(user);
        }

        public async Task<LoginResponse?> Login(LoginRequest request)
        {
            User user = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(
               predicate: x => x.Username.Equals(request.Username)
               );

            if (user == null || !PasswordUtil.VerifyHashedPassword(user.Password, request.Password))
                return null;

            return new LoginResponse()
            {
                TokenModel = JwtUtil.GenerateJwtToken(user),
                UserId = user.Id,
                Username = user.Username,
                Role = EnumUtil.ParseEnum<RoleEnum>(user.Role),
                Status = EnumUtil.ParseEnum<UserStatus>(user.Status),
            };

        }

    }
}
