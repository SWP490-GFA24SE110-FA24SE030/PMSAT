using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            return await _context.Projects.Include(t => t.TaskPs).ToListAsync();
        }

        public async Task<Project?> GetByIdAsync(Guid id)
        {
            return await _context.Projects.Include(t => t.TaskPs).FirstOrDefaultAsync(i => i.Id == id);
        }

        public Task<bool> ProjectExist(Guid id)
        {
            return _context.Projects.AnyAsync(p => p.Id == id);
        }

        public Task<Project?> UpdateByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}