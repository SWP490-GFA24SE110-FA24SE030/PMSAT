using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class TaskSprintRepository : ITaskSprintRepository
    {
        private readonly PmsatContext _context;

        public TaskSprintRepository(PmsatContext context)
        {
            _context = context;   
        }

        public async Task<TaskSprint> Create(TaskSprint taskSprintModel)
        {
            await _context.TaskSprints.AddAsync(taskSprintModel);
            await _context.SaveChangesAsync();
            return taskSprintModel;
            
        }

        public async Task<List<TaskP>> GetSprintTask(Sprint sprint)
        {
            return await _context.TaskSprints.Where(x => x.SprintId == sprint.Id)
            .Select(task => new TaskP
            {
                Id = task.Id,
                Status = task.Task.Status,
                Description = task.Task.Description,
                StartDate = task.Task.StartDate,
                EndDate = task.Task.EndDate
            }).ToListAsync();
        }

        public async Task<TaskSprint> RemoveTask(TaskP task, Guid sprintId)
        {
            var taskModel    = await _context.TaskSprints.FirstOrDefaultAsync(x => x.Task == task && x.SprintId == sprintId);
            if (taskModel == null) 
            {
                return null;
            }
            _context.Remove(taskModel);
            await _context.SaveChangesAsync();
            return taskModel;
        }
    }
}