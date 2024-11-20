using api.Dtos.ProjectMember;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class ProjectMemberRepository : IProjectMemberRepository
    {
        private readonly PmsatContext _context;

        public ProjectMemberRepository(PmsatContext context)
        {
            _context = context;
        }

        public async Task<bool> AddProjectMemberAsync(ProjectMemberDto projectMemberDto)
        {
            // Check if the user and project exist in the database
            var userExists = await _context.Users.AnyAsync(u => u.Id == projectMemberDto.UserId);
            var projectExists = await _context.Projects.AnyAsync(p => p.Id == projectMemberDto.ProjectId);
            if (!userExists || !projectExists)
                return false;

            // Create a new ProjectMember entity
            var projectMember = new ProjectMember
            {
                Id = Guid.NewGuid(),
                Role = projectMemberDto.Role,
                UserId = projectMemberDto.UserId,
                ProjectId = projectMemberDto.ProjectId
            };

            // Add to the database
            _context.ProjectMembers.Add(projectMember);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ProjectMemberDto>> GetProjectMembersAsync(Guid projectId)
        {
            return await _context.ProjectMembers
                .Where(pm => pm.ProjectId == projectId) // Filter by project ID
                .Select(pm => new ProjectMemberDto
                {
                    UserId = pm.Id,
                    ProjectId = projectId,
                    Role = pm.Role,
                })
                .ToListAsync();
        }
    }
}
