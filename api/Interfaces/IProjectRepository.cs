using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Project;
using api.Models;

namespace api.Interfaces
{
    public interface IProjectRepository
    {
        Task<List<Project>> GetAllAsync();
        Task<Project?> GetByIdAsync(Guid id);
        Task<Project> CreateAsync(Project projectModel);
        Task<Guid> CreateProjectAsync(Guid userId, CreateProjectRequestDto createProjectDto);
        Task<Project?> UpdateByIdAsync(Guid id, UpdateProjectRequestDto projectDto);
        Task<Project> DeleteByIdAsync(Guid id);
        Task<List<Project>> DeleteAllAsync();
        Task<bool> ProjectExist(Guid id);
        Task<List<Project>> GetByTitleAsync(string title);
    }
}