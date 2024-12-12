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
                return BadRequest(new { message = "Failed to add project member. Ensure the user exists and the project ID is valid." });

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
        [Route("delete/prjmemberid={id}")]
        public async Task<IActionResult> DeleteByIdAsync([FromRoute] Guid id)
        {
            var projectMemberModel = await _projectMemberRepository.DeleteByIdAsync(id);

            return Ok(new { Message = "Project Member(s) deleted successfully." });
        }
    }
}
