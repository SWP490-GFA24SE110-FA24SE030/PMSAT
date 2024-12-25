using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Sprint;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class SprintRepository : ISprintRepository
    {
        private readonly PmsatContext _context;
        
        public SprintRepository (PmsatContext context)
        {
            _context = context;
            
        }
        public async Task<Sprint> CreateAsync(Sprint sprintModel)
        {
            
            await _context.Sprints.AddAsync(sprintModel);
            await _context.SaveChangesAsync();
            return sprintModel;
        }

        public async Task<List<Sprint>> GetAllAsync()
        {
            return await _context.Sprints.Include(t => t.TaskPs).ToListAsync();
        }

        public async Task<List<Sprint>> GetProjectSprint(Guid projectId)
        {
            if (projectId == Guid.Empty)
            {
                throw new ArgumentException("Invalid Id provided", nameof(projectId));
            }

            return await _context.Sprints.Where(x => x.ProjectId == projectId).Include(t => t.TaskPs).ToListAsync();
        }

        public async Task<Sprint?> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Invalid Id provided", nameof(id));
            }

            return await _context.Sprints.FindAsync(id);
        }

        public async Task<Sprint> GetByNameAsync(string sprintName)
        {
            return await _context.Sprints.FirstOrDefaultAsync(x => x.Name == sprintName);
        }

        public async Task<TaskP> AddTaskToSprint(Guid sprintId, Guid taskId)
        {
            var task = await _context.TaskPs.FirstOrDefaultAsync(t => t.Id == taskId);
            task.SprintId = sprintId;
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<Sprint> DeleteByIdAsync(Guid id)
        {
            var sprint = await _context.Sprints.FirstOrDefaultAsync(s => s.Id == id);
            _context.Sprints.Remove(sprint);
            await _context.SaveChangesAsync();
            return sprint;
        }

        public async Task<TaskP> RemoveTaskFromSprint(Guid taskId)
        {
            var task = await _context.TaskPs.FirstOrDefaultAsync(t => t.Id == taskId);
            if (task == null) 
            {
                return null;
            }
            task.SprintId = null;
            await _context.SaveChangesAsync();
            return task;
        }
    }
}