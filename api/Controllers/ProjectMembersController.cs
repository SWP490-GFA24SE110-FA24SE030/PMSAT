using api.Dtos.ProjectMember;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/projectmember")]
    public class ProjectMembersController : ControllerBase
    {
        private readonly IProjectMemberRepository _projectMemberRepository;

        public ProjectMembersController(IProjectMemberRepository projectMemberRepository)
        {
            _projectMemberRepository = projectMemberRepository;
        }

        [HttpPost("prjid={projectId}/add")]
        public async Task<IActionResult> AddProjectMember([FromRoute] Guid projectId, [FromBody] AddProjectMemberRequest projectMemberDto)
        {
            var success = await _projectMemberRepository.AddProjectMemberAsync(projectId, projectMemberDto);

            if (!success)
            {
                var projectExists = await _projectMemberRepository.ProjectExistsAsync(projectId);
                if (!projectExists)
                {
                    return NotFound(new { message = "Project not found." });
                }

                var user = await _projectMemberRepository.GetUserByEmailAsync(projectMemberDto.Email);
                if (user == null)
                {
                    return NotFound(new { message = "User not found." });
                }

                var isUserAlreadyMember = await _projectMemberRepository.IsUserAlreadyMemberAsync(projectId, user.Id);
                if (isUserAlreadyMember)
                {
                    return Conflict(new { message = "This member's email has been already added in the project." });
                }
            }    

            return Ok(new { message = "Project member added successfully." });
        }

        [HttpGet("prjid={projectId}/all")]
        public async Task<IActionResult> GetProjectMembersFromProject([FromRoute] Guid projectId)
        {
            try
            {
                var projectMembers = await _projectMemberRepository.GetProjectMembersFromProjectAsync(projectId);

                // Convert to DTOs if necessary
                var projectMemberDtos = projectMembers.Select(pm => pm.ToProjectMemberDto()).ToList();

                return Ok(projectMembers);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving project members.", error = ex.Message });
            }
        }

        [HttpDelete]
        [Route("uid/{userId}/from/prjid/{projectId}/DeleteMemberFromProject")]
        public async Task<IActionResult> DeleteByIdAsync([FromRoute] Guid userId, [FromRoute] Guid projectId)
        {
            var projectMemberModel = await _projectMemberRepository.DeleteByIdAsync(userId, projectId);

            return Ok(new { Message = "Project Member(s) deleted successfully." });
        }
    }
}
