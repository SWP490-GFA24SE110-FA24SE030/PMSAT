using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Task;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<string> AssignTaskToMemberAsync(Guid userId, Guid taskId, string email)
        {
            // Validate if the current user is a Project Leader
            var leader = await _context.ProjectMembers.FirstOrDefaultAsync(pm =>
            pm.UserId == userId && pm.Role == "Leader");

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
            task.AssigneeId = projectMember.UserId;
            task.ReporterId = leader.UserId;
            task.Updated = DateTime.Now;
            
            await _context.SaveChangesAsync();
            return "Success"; 
        }

        public async Task<Board> ChangeBoard(Guid taskId, Guid boardId)
        {
            var task = await _context.TaskPs.FirstOrDefaultAsync(t => t.Id == taskId);
            task.BoardId = boardId;
            await _context.SaveChangesAsync();
            return await _context.Boards.FirstOrDefaultAsync(b => b.Id == boardId);
        }

        public async Task<TaskP> CreateAsync(TaskP taskModel)
        {
            await _context.TaskPs.AddAsync(taskModel);
            await _context.SaveChangesAsync();
            return taskModel;
        }

        public async Task<TaskP> DeleteByIdAsync(Guid id)
        {
            var taskModel = await _context.TaskPs.FirstOrDefaultAsync(x => x.Id == id);

            _context.TaskPs.Remove(taskModel);

            await _context.SaveChangesAsync();

            return taskModel;
        }

        public async Task<List<TaskP>> GetAllAsync()
        {
            return await _context.TaskPs
                .Include(t => t.Issues)
                .Include(t => t.Tags)
                .ToListAsync();
        }

        public async Task<TaskP> GetByIdAsync(Guid id)
        {
            var task = await _context.TaskPs
                    .Where(t => t.Id == id)
                    .Include(t => t.Issues)
                    .FirstOrDefaultAsync();
            return task;
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
                .ToListAsync();

            return tasks;
        }

        public async Task<List<TaskP>> GetTasksFromSprintAsync(Guid sprintId)
        {
            return await _context.TaskPs.Where(t => t.SprintId == sprintId).ToListAsync();
        }

        public async Task UpdateAsync(TaskP task)
        {
            _context.TaskPs.Update(task);
            await _context.SaveChangesAsync();
        }

        public async Task<string> UpdateTaskStatusAsync(Guid taskId, string status)
        { 
            var task = await _context.TaskPs.FirstOrDefaultAsync(t => t.Id == taskId);
            
            task.Status = status;
            await _context.SaveChangesAsync();
            return status;
        }
    }
}