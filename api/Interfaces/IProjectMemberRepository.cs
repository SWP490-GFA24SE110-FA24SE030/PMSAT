using api.Dtos.ProjectMember;
using api.Models;

namespace api.Interfaces
{
    public interface IProjectMemberRepository
    {
        Task<bool> AddProjectMemberAsync(Guid projectId, AddProjectMemberRequest projectMemberDto);
        Task<List<ProjectMember>> GetProjectMembersFromProjectAsync(Guid projectId);
        Task<ProjectMember> DeleteByIdAsync(Guid id);
    }
}
