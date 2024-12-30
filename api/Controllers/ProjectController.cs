using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api.Dtos.Project;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{
    [ApiController]
    [Route("api/project")]
    
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _projectRepo;

        public ProjectController(IProjectRepository projectRepo)
        {
            _projectRepo = projectRepo;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll() 
        {
            var projects = await _projectRepo.GetAllAsync(); 

            //var projectDto = projects.Select(s => s.ToProjectDto());

            return Ok(projects);
        }

        [HttpGet("uid={userId}/all")]
        public async Task<IActionResult> GetProjectsByUserId([FromRoute] Guid userId)
        {
            try
            {
                var projects = await _projectRepo.GetAllProjectsByUserIdAsync(userId);

                if (!projects.Any())
                {
                    return NotFound(new { message = "No projects found for the user." });
                }

                return Ok(projects);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching projects.", error = ex.Message });
            }
        }

        [HttpGet("prjid={id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var project = await _projectRepo.GetByIdAsync(id);

            if (project == null) 
            {
                return NotFound();
            }

            return Ok(project);
        }

        [HttpGet("title={title}")]
        public async Task<IActionResult> GetByTitle([FromRoute] string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return BadRequest("Project title cannot be empty.");
            }

            try
            {
                var project = await _projectRepo.GetByTitleAsync(title);

                if (!project.Any())
                {
                    return NotFound($"Project with title '{title}' not found.");
                }

                return Ok(project);
            }
            catch (Exception)
            {
                return StatusCode(500, "An internal server error occurred.");
            }
        }

        [Authorize]
        [HttpPost("uid=new")]
        public async Task<IActionResult> CreateProject( [FromBody] CreateProjectRequestDto createProjectDto)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            try
            {
                var newProjectId = await _projectRepo.CreateProjectAsync(userId, createProjectDto);

                return CreatedAtAction(nameof(GetById), new { id = newProjectId }, new { message = "Project created successfully.", projectId = newProjectId });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the project.", error = ex.Message });
            }
        }

        [HttpPut]
        [Route("edit/prjid={id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateProjectRequestDto updateProjectRequestDto)
        {
            var projectModel = await _projectRepo.UpdateByIdAsync(id, updateProjectRequestDto);

            if (projectModel == null)
            {
                return NotFound();
            }
            return Ok(new { Message = "Project(s) updated successfully." });
        }

        [HttpDelete]
        [Route("delete/prjid={id}")]
        public async Task<IActionResult> DeleteByIdAsync([FromRoute] Guid id)
        {
            var projectModel = await _projectRepo.DeleteByIdAsync(id);

            return Ok(new { Message = "Project(s) deleted successfully." });
        }

        [HttpDelete]
        [Route("delete/all")]
        public async Task<IActionResult> DeleteAllAsync()
        {
            var projectModel = await _projectRepo.DeleteAllAsync();

            return Ok(new { Message = "Project(s) deleted successfully." });
        }
    }
}