using PMSAT.BusinessTier.Payload.Login;
using PMSAT.BusinessTier.Payload.Users;
using PMSAT.BusinessTier.Payload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSAT.BusinessTier.Services.Interfaces
{
    public interface IUserService
    {
        Task<Guid> CreateNewUser(CreateNewUserRequest request);
        Task<GetUserReponse> GetUserDetail(Guid userId);
        Task<LoginResponse?> Login(LoginRequest request);
    }
}
