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

        public async Task<ProjectMember> DeleteByIdAsync(Guid id)
        {
            var projectMemberModel = await _context.ProjectMembers.FirstOrDefaultAsync(x => x.Id == id);

            _context.ProjectMembers.Remove(projectMemberModel);

            await _context.SaveChangesAsync();

            return projectMemberModel;
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
    }
}
