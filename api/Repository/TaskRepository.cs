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

        public async Task<string> AssignTaskToMemberAsync(AssignTaskToMemberDto assignTaskToMemberDto)
        {
            // Validate if the current user is a Project Leader
            var leader = await _context.ProjectMembers.FirstOrDefaultAsync(
                pm => pm.Id == assignTaskToMemberDto.LeaderID
                && pm.Role == "Leader");
            if (leader == null)
            {
                return "You are not authorized to assign tasks.";
            }

            // Validate that the Task exists and belongs to the same Project as the leader
            var task = await _context.TaskPs.FirstOrDefaultAsync(
                t => t.Id == assignTaskToMemberDto.TaskId);
            if (task == null)
            {
                return "Task not found.";
            }
            if (task.ProjectId != leader.ProjectId)
            {
                return "You can only assign tasks within projects you manage.";
            }

            // Validate the Project Member to assign the task to
            var projectMember = await _context.ProjectMembers.FirstOrDefaultAsync(
                pm => pm.ProjectId == task.ProjectId
                && pm.Id == assignTaskToMemberDto.MemberID);
            if (projectMember == null)
            {
                return "Project member not found or does not belong to this project.";
            }

            // Assign the task to the Project Member
            task.ProjectMemberId = projectMember.Id; // Link task to project member
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

        
    }
}