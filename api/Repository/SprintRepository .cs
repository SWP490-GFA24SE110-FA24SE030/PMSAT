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

            return await _context.Sprints.Where(x => x.ProjectId == projectId).Include(t => t.TaskPs).OrderByDescending(s => s.EndDate).ToListAsync();
        }

        public async Task<Sprint?> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                {
                    throw new ArgumentException("Invalid Id provided", nameof(id));
                }

            var sprint = await _context.Sprints
                .Include(s => s.TaskPs)
                .FirstOrDefaultAsync(s => s.Id == id);

            return sprint;
        }

        public async Task addTaskFromSprintToBoard(Guid sprintId)
        {
            var tasks = await _context.TaskPs.Where(t => t.SprintId == sprintId).ToListAsync(); //_taskRepo.GetTasksFromSprintAsync(sprintId);
            var boards = await _context.Boards.ToListAsync();
            foreach (var task in tasks) {
                foreach (var board in boards)
                {
                    if (board.Status == task.Status && board.ProjectId == task.ProjectId)
                    {
                        if (!board.TaskPs.Contains(task))
                        {
                            board.TaskPs.Add(task); 
                            Console.WriteLine($"Task '{task.Title}' added to Board with Status '{board.Status}'.");
                        }
                        else
                        {
                            Console.WriteLine($"Task '{task.Title}' is already in the Board.");
                        }
                    }
                }
            }
            await _context.SaveChangesAsync();
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
            var tasks = _context.TaskPs.Where(t => t.SprintId == id).ToList();
            foreach (var task in tasks)
            {
                task.SprintId = null;  // Disassociate the task from the sprint
            }
            await _context.SaveChangesAsync();

            var sprint = await _context.Sprints.FindAsync(id);
            if (sprint != null)
            {
                _context.Sprints.Remove(sprint);
                await _context.SaveChangesAsync();
            }
            
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

        public async Task UpdateSprintDate(Guid sprintId, DateTime startDate, DateTime endDate)
        {
            var task = await _context.Sprints.FirstOrDefaultAsync(s => s.Id == sprintId);
            task.StartDate = startDate;
            task.EndDate = endDate;
            await _context.SaveChangesAsync();
        }

        public async Task RemoveSprintDate(Guid sprintId)
        {
            var task = await _context.Sprints.FirstOrDefaultAsync(s => s.Id == sprintId);
            task.StartDate = null;
            task.EndDate = null;
            await _context.SaveChangesAsync();
        }
    }
}