using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api.Dtos.AuthDto;
using api.Dtos.User;
using api.Interfaces;
using api.Mappers;
using api.Models;
using api.Services;
using Microsoft.AspNetCore.Authorization;
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
        private readonly string _profilePicturesPath = @"C:\Users\ASUS\Desktop\UserAvatar";

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

        [Authorize]
        [HttpGet("profile")]
        public async Task<ActionResult<User>> GetProfile()
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
                

            if (user == null)
                return NotFound();

            user.Password = null;
            return user;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _context.Users
                .AsNoTracking()
                .Include(u => u.Role)
                .ToListAsync();

            return Ok(users);
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

        [Authorize]
        [HttpPost("avatar")]
        public async Task<IActionResult> UploadProfilePicture(IFormFile profilePicture)
        {
            if (profilePicture == null || profilePicture.Length == 0)
            {
                return BadRequest("No file was uploaded.");
            }

            // Ensure the directory exists
            if (!Directory.Exists(_profilePicturesPath))
            {
                Directory.CreateDirectory(_profilePicturesPath);
            }

            // Generate a unique filename
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(profilePicture.FileName);
            var filePath = Path.Combine(_profilePicturesPath, fileName);

            // Save the file to the server
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await profilePicture.CopyToAsync(stream);
            }

            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _userRepo.SetAvatar(userId, $"/profile_pictures/{fileName}");

            return Ok(new { FilePath = $"/profile_pictures/{fileName}" });
        }


    }
}