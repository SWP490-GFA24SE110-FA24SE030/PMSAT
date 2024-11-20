using api.Dtos.ProjectMember;
using api.Interfaces;
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

        [HttpPost("add")]
        public async Task<IActionResult> AddProjectMember([FromBody] ProjectMemberDto projectMemberDto)
        {
            var success = await _projectMemberRepository.AddProjectMemberAsync(projectMemberDto);

            if (!success)
                return BadRequest(new { message = "Failed to add project member." });

            return Ok(new { message = "Project member added successfully." });
        }

        [HttpGet("prjid={projectId}/members")]
        public async Task<IActionResult> GetProjectMembers([FromRoute] Guid projectId)
        {
            // Get all project members
            var members = await _projectMemberRepository.GetProjectMembersAsync(projectId);

            if (members == null || !members.Any())
                return NotFound(new { message = "No project members found." });

            return Ok(members);
        }
    }
}
