using api.Dtos.ProjectMember;

namespace api.Interfaces
{
    public interface IProjectMemberRepository
    {
        Task<bool> AddProjectMemberAsync(ProjectMemberDto projectMemberDto);
        Task<IEnumerable<ProjectMemberDto>> GetProjectMembersAsync(Guid projectId);
    }
}
