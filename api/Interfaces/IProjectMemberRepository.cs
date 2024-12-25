using api.Dtos.ProjectMember;
using api.Models;

namespace api.Interfaces
{
    public interface IProjectMemberRepository
    {
        Task<bool> AddProjectMemberAsync(Guid projectId, AddProjectMemberRequest projectMemberDto);
        Task<bool> ProjectExistsAsync(Guid projectId);
        Task<User> GetUserByEmailAsync(string email);
        Task<bool> IsUserAlreadyMemberAsync(Guid projectId, Guid userId);
        Task<List<ProjectMember>> GetProjectMembersFromProjectAsync(Guid projectId);
        Task<ProjectMember> DeleteByIdAsync(Guid userId, Guid projectId);
    }
}
