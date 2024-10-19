using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMSAT.API.Controllers;
using PMSAT.BusinessTier.Constants;
using PMSAT.BusinessTier.Enums;
using PMSAT.BusinessTier.Error;
using PMSAT.BusinessTier.Payload.Login;
using PMSAT.BusinessTier.Services.Interfaces;
using PMSAT.BusinessTier.Payload.Users;
using PMSAT.BusinessTier.Validators;

namespace PMSAT.API.Controllers
{
    [ApiController]
    public class AuthenticationController : BaseController<AuthenticationController>
    {
        private readonly IUserService _userService;

        public AuthenticationController(ILogger<AuthenticationController> logger, IUserService userService) : base(logger)
        {
            _userService = userService;

        }

        [HttpPost(ApiEndPointConstant.Authentication.Login)]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(UnauthorizedObjectResult))]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var loginResponse = await _userService.Login(loginRequest);
            if (loginResponse == null)
            {
                return Unauthorized(new ErrorResponse()
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Error = MessageConstant.LoginMessage.InvalidUsernameOrPassword,
                    TimeStamp = DateTime.Now
                });
            }
            if (loginResponse.Status == UserStatus.Deactivate)
                return Unauthorized(new ErrorResponse()
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Error = MessageConstant.LoginMessage.DeactivatedAccount,
                    TimeStamp = DateTime.Now
                });
            return Ok(loginResponse);
        }
    }
}
