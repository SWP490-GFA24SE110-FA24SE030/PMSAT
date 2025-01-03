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
            if (!await ProjectExistsAsync(projectId))
                return false;

            // Find the user by email
            var user = await GetUserByEmailAsync(projectMemberDto.Email);
            if (user == null)
                return false;

            // Check if the user is already a member of the project
            if (await IsUserAlreadyMemberAsync(projectId, user.Id))
                return false;

            // Create a new ProjectMember entity
            var projectMember = new ProjectMember
            {
                Role = "Member",
                UserId = user.Id, // Set the UserId from the found user
                ProjectId = projectId
            };

            // Add the new project member to the database
            _context.ProjectMembers.Add(projectMember);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ProjectMember> DeleteByIdAsync(Guid userId, Guid projectId)
        {
            // Find the ProjectMember by UserId and ProjectId
            var projectMember = await _context.ProjectMembers
                .FirstOrDefaultAsync(pm => pm.UserId == userId && pm.ProjectId == projectId);

            _context.ProjectMembers.Remove(projectMember);
            await _context.SaveChangesAsync();
            return projectMember;
        }

        public async Task<List<ProjectMember>> GetProjectMembersFromProjectAsync(Guid projectId)
        {
            var projectExists = await _context.Projects.AnyAsync(p => p.Id == projectId);
            if (!projectExists)
            {
                throw new ArgumentException("Project does not exist.");
            }

            // Fetch all members associated with the project
            var projectMembers = await _context.ProjectMembers
                .Where(pm => pm.ProjectId == projectId)
                .Include(pm => pm.User) // Include related User information if needed
                .ToListAsync();

            return projectMembers;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> IsUserAlreadyMemberAsync(Guid projectId, Guid userId)
        {
            return await _context.ProjectMembers.AnyAsync(pm => pm.ProjectId == projectId && pm.UserId == userId);
        }

        public async Task<bool> ProjectExistsAsync(Guid projectId)
        {
            return await _context.Projects.AnyAsync(p => p.Id == projectId);
        }
    }
}
