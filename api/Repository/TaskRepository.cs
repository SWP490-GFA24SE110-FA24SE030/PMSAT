using System;
using System.Collections.Generic;
using System.Linq;
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