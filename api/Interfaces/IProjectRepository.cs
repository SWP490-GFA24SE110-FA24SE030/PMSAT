using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IProjectRepository
    {
        Task<List<Project>> GetAllAsync();
        Task<Project?> GetByIdAsync(Guid id);
        Task<Project> CreateAsync(Project projectModel);
        Task<Project?> UpdateByIdAsync(Guid id);
        Task<Project> DeleteByIdAsync(Guid id);
        Task<bool> ProjectExist(Guid id);
    }
}