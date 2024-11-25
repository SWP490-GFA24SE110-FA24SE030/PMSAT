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

        public async Task<bool> AddProjectMemberAsync(Guid projectId, AddProjectMemberRequest projectMemberDto)
        {
            // Check if the project exists
            var projectExists = await _context.Projects.AnyAsync(p => p.Id == projectId);
            if (!projectExists)
                return false;

            // Find the user by email
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == projectMemberDto.Email);
            if (user == null)
                return false;

            // Create a new ProjectMember entity
            var projectMember = new ProjectMember
            {
                Id = Guid.NewGuid(),
                Role = projectMemberDto.Role,
                UserId = user.Id, // Set the UserId from the found user
                ProjectId = projectId
            };

            // Add the new project member to the database
            _context.ProjectMembers.Add(projectMember);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
