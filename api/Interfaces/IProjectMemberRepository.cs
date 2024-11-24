using api.Dtos.ProjectMember;

namespace api.Interfaces
{
    public interface IProjectMemberRepository
    {
        Task<bool> AddProjectMemberAsync(Guid projectId, ProjectMemberDto projectMemberDto);
    }
}
