using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Sprint;
using api.Interfaces;
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
            return await _context.Sprints.ToListAsync();
        }

        public async Task<Sprint?> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Invalid Guid provided", nameof(id));
            }

            return await _context.Sprints.FindAsync(id);
        }

        public async Task<Sprint> GetByNameAsync(string sprintName)
        {
            return await _context.Sprints.FirstOrDefaultAsync(x => x.Name == sprintName);
        }
    }
}