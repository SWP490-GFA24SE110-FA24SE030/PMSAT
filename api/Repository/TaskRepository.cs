using System;
using System.Collections.Generic;
using System.Linq;
using api.Dtos.Task;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;



namespace api.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly PmsatContext _context;

        public TaskRepository(PmsatContext context)
        {
            _context = context;
        }

        public async Task<string> AssignTaskToMemberAsync(Guid leaderId, Guid taskId, string email)
        {
            // Validate if the current user is a Project Leader
            var leader = await _context.ProjectMembers.FirstOrDefaultAsync(pm =>
            pm.Id == leaderId && pm.Role == "Leader");

            if (leader == null)
            {
                return "You are not authorized to assign tasks.";
            }

            // Validate that the Task exists and belongs to the same Project as the leader
            var task = await _context.TaskPs.FirstOrDefaultAsync(t => t.Id == taskId);

            if (task == null)
            {
                return "Task not found.";
            }
            if (task.ProjectId != leader.ProjectId)
            {
                return "You can only assign tasks within projects you manage.";
            }

            // Validate if the project member exists using their email
            var projectMember = await _context.ProjectMembers
                .Include(pm => pm.User) // Include the User entity to access the email
                .FirstOrDefaultAsync(pm =>
                    pm.ProjectId == task.ProjectId &&
                    pm.User.Email == email);

            if (projectMember == null)
            {
                return "Project member not found or does not belong to this project.";
            }

            // Assign the task to the Project Member
            task.ProjectMemberId = projectMember.Id; // Update task with project member ID
            await _context.SaveChangesAsync();
            return "Success"; 
        }

        public async Task<TaskP> CreateAsync(TaskP taskModel)
        {
            await _context.TaskPs.AddAsync(taskModel);
            await _context.SaveChangesAsync();
            return taskModel;
        }

        public async Task<List<TaskP>> GetAllAsync()
        {
            return await _context.TaskPs.ToListAsync();
        }

        public async Task<TaskP> GetByIdAsync(Guid id)
        {
            return await _context.TaskPs.FindAsync(id);
        }

        public async Task<List<TaskP>> GetTasksFromProjectAsync(Guid projectId)
        {
            // Validate if the project exists
            var projectExists = await _context.Projects.AnyAsync(p => p.Id == projectId);
            if (!projectExists)
            {
                throw new ArgumentException("Project does not exist.");
            }

            // Fetch all tasks related to the project
            var tasks = await _context.TaskPs
                .Where(t => t.ProjectId == projectId)
                .Include(t => t.Workflows) // to display task's status workflows
                .ToListAsync();

            return tasks;
        }
    }
}