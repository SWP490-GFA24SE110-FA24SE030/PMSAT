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

        [HttpPost("prjid={projectId}/add")]
        public async Task<IActionResult> AddProjectMember([FromRoute] Guid projectId, [FromBody] AddProjectMemberRequest projectMemberDto)
        {
            var success = await _projectMemberRepository.AddProjectMemberAsync(projectId, projectMemberDto);

            if (!success)
                return BadRequest(new { message = "Failed to add project member. Ensure the user exists and the project ID is valid." });

            return Ok(new { message = "Project member added successfully." });
        }
    }
}
