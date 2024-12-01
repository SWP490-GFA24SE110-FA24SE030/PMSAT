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
        public async Task<List<TaskP>> GetSprintTask(Sprint sprint)
        {
            return await _context.TaskSprints.Where(x => x.SprintId == sprint.Id)
            .Select(task => new TaskP
            {
                Id = task.Id,
                Type = task.Task.Type,
                Description = task.Task.Description,
                StartDate = task.Task.StartDate,
                EndDate = task.Task.EndDate
            }).ToListAsync();
        }
    }
}