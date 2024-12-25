using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.AuthDto;
using api.Dtos.User;
using api.Interfaces;
using api.Mappers;
using api.Models;
using api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly PmsatContext _context;
        private readonly IUserRepository _userRepo;

        private readonly IAuthService _authService;
        public UserController(PmsatContext context, IUserRepository userRepo, IAuthService authService)
        {
            _userRepo = userRepo;
            _context = context;
            _authService = authService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll() 
        {
            var users = await _userRepo.GetAllAsync();

            var userDto =  users.Select(s => s.ToUserDto());

            return Ok(users);
        }

        [HttpGet("uid={id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id) 
        {
            var user = await _userRepo.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user.ToUserDto());
        }

        [HttpPost("new")]
        public async Task<IActionResult> Create(RegisterRequest request)
        {
            try
            {
                var response = await _authService.Register(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut]
        [Route("update/uid={id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateUserRequestDto updateDto)
        {
            var userModel = await _userRepo.UpdateAsync(id, updateDto);

            if(userModel == null) 
            {
                return NotFound();
            }

            

            return Ok(userModel.ToUserDto());
        }

        [HttpDelete]
        [Route("delete/uid={id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id) 
        {
            var userModel = await _userRepo.DeleteAsync(id);

            if(userModel == null) 
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}