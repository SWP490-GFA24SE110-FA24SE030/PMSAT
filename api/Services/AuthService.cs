using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.LoginDto;

namespace api.Services
{
    public class AuthService : IAuthService
    {
        public Task<LoginResponse> Login(LoginRequest request)
        {
            throw new NotImplementedException();
        }
    }
}