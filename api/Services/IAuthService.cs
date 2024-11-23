using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.AuthDto;
using api.Dtos.LoginDto;

namespace api.Services
{
    public interface IAuthService
    {
        Task<LoginResponse> Login(LoginRequest request);
        Task<RegisterResponse> Register(RegisterRequest request);
    }
}