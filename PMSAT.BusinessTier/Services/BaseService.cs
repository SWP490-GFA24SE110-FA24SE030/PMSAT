using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using PMSAT.BusinessTier.Extensions;
using PMSAT.DataTier.Models;
using PMSAT.Repository.Interfaces;

namespace PMSAT.BusinessTier.Services
{
    public abstract class BaseService<T> where T : class
    {
        protected IUnitOfWork<PmsatContext> _unitOfWork;
        protected ILogger<T> _logger;
        protected IMapper _mapper;
        protected IHttpContextAccessor _httpContextAccessor;

        protected BaseService(IUnitOfWork<PmsatContext> unitOfWork, ILogger<T> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        protected string GetAccessTokenFromJwt()
        {
            return _httpContextAccessor.HttpContext.GetJwtToken();
        }

        protected string GetUsernameFromJwt()
        {
            string username = _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return username;
        }

        protected string GetUserIdFromJwt()
        {
            string userId = _httpContextAccessor?.HttpContext?.User.FindFirstValue("userId");
            return userId;
        }

        protected string GetRoleFromJwt()
        {
            string role = _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.Role);
            return role;
        }

    }
}

