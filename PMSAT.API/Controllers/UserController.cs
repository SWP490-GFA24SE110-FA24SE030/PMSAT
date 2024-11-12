using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMSAT.API.Controllers;
using PMSAT.BusinessTier.Constants;
using PMSAT.BusinessTier.Payload.Users;
using PMSAT.BusinessTier.Payload;
using PMSAT.BusinessTier.Services.Interfaces;
using PMSAT.BusinessTier.Enums;
using PMSAT.BusinessTier.Validators;

namespace PMSAT.API.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        //[CustomAuthorize(RoleEnum.Admin)]
        [HttpPost(ApiEndPointConstant.User.UsersEndPoint)]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(UnauthorizedObjectResult))]
        public async Task<IActionResult> CreateNewUser(CreateNewUserRequest request)
        {
            var response = await _userService.CreateNewUser(request);
            return Ok(response);
        }

        //[CustomAuthorize(RoleEnum.Admin)]
        [HttpGet(ApiEndPointConstant.User.UserEndPoint)]
        [ProducesResponseType(typeof(GetUserReponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserDetail(Guid id)
        {
            var response = await _userService.GetUserDetail(id);
            return Ok(response);
        }

        
    }
}
