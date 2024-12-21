using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Project;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly PmsatContext _context;

        public ProjectRepository(PmsatContext context)
        {
            _context = context;
        }
        public async Task<Project> CreateAsync(Project projectModel)
        {
            await _context.Projects.AddAsync(projectModel);
            await _context.SaveChangesAsync();
            return projectModel;
        }

        public async Task<Guid> CreateProjectAsync(Guid userId, CreateProjectRequestDto createProjectDto)
        {
            // Check if the user exists
            var userExists = await _context.Users.AnyAsync(u => u.Id == userId);
            if (!userExists)
            {
                throw new ArgumentException("User does not exist.");
            }

            // Create a new project
            var newProject = new Project
            {
                Id = Guid.NewGuid(),
                Title = createProjectDto.Title,
            };

            // Add the project to the database
            _context.Projects.Add(newProject);
            await _context.SaveChangesAsync();

            // Create a new ProjectMember entry for the user (who created project) as the Leader
            var newProjectMember = new ProjectMember
            {
                Role = "Leader",
                UserId = userId,
                ProjectId = newProject.Id
            };

            // Add the project member to the database
            _context.ProjectMembers.Add(newProjectMember);
            await _context.SaveChangesAsync();

            // Create a new default Board (To-Do ; In Progress ; Done)
            var defaultBoards = new List<Board>
            {
                new Board
                {
                    Id = Guid.NewGuid(),
                    Orders = 1,
                    Status = "To-Do",
                    ProjectId = newProject.Id
                },
                new Board
                {
                    Id = Guid.NewGuid(),
                    Orders = 2,
                    Status = "In Progress",
                    ProjectId = newProject.Id
                },
                new Board
                {
                    Id = Guid.NewGuid(),
                    Orders = 3,
                    Status = "Done",
                    ProjectId = newProject.Id
                }
            };

            // Add the default boards to the database
            await _context.Boards.AddRangeAsync(defaultBoards);
            await _context.SaveChangesAsync();

            return newProject.Id;
        }

        public async Task<List<Project>> DeleteAllAsync()
        {
            // Get all projects
            var projectList = await _context.Projects.ToListAsync();

            // Remove all projects
            _context.Projects.RemoveRange(projectList);

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return the list of deleted projects
            return projectList;
        }

        public async Task<Project> DeleteByIdAsync(Guid id)
        {
            var projectModel = await _context.Projects.FirstOrDefaultAsync(x => x.Id == id);

            if (projectModel == null) 
            {
                return null;
            }

            _context.Projects.Remove(projectModel);

            await _context.SaveChangesAsync();

            return projectModel;
        }

        public async Task<List<Project>> GetAllAsync()
        {
            return await _context.Projects
                .Include(a => a.AnalysisResults)
                .Include(b => b.Boards)
                .Include(pm => pm.ProjectMembers)
                .Include(s => s.Sprints)
                .Include(t => t.TaskPs)
                .ToListAsync();
        }

        public async Task<List<Project>> GetAllProjectsByUserIdAsync(Guid userId)
        {
            var userExists = await _context.Users.AnyAsync(u => u.Id == userId);
            if (!userExists)
            {
                throw new ArgumentException("User does not exist.");
            }

            // Query projects associated with the user through the ProjectMember table
            var projectMembers = await _context.ProjectMembers
                .Where(pm => pm.UserId == userId)
                .Include(pm => pm.Project) // Include the associated Project
                .ToListAsync();

            return projectMembers.Select(pm => pm.Project).ToList();
        }

        public async Task<Project?> GetByIdAsync(Guid id)
        {
            return await _context.Projects
                .Include(a => a.AnalysisResults)
                .Include(b => b.Boards)
                .Include(pm => pm.ProjectMembers)
                .Include(s => s.Sprints)
                .Include(t => t.TaskPs)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<List<Project>> GetByTitleAsync(string title)
        {
            // Ensure case-insensitive search
            return await _context.Projects
                .Where(p => EF.Functions.Like(p.Title, $"%{title}%"))
                .ToListAsync();
        }

        public Task<bool> ProjectExist(Guid id)
        {
            return _context.Projects.AnyAsync(p => p.Id == id);
        }

        public async Task<Project?> UpdateByIdAsync(Guid id, UpdateProjectRequestDto projectDto)
        {
            // Retrieve the project from the database
            var existingProject = await _context.Projects.FirstOrDefaultAsync(x => x.Id == id);

            if (existingProject == null)
            {
                return null;
            }

            existingProject.Title = projectDto.Title;

            await _context.SaveChangesAsync();
            return existingProject;
        }
    }
}