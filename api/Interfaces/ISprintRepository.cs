using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Sprint;
using api.Models;

namespace api.Interfaces
{
    public interface ISprintRepository
    {
        public Task<Sprint> CreateAsync(Sprint sprintModel);
        public Task<Sprint> GetByIdAsync(Guid id);
        public Task<Sprint> DeleteByIdAsync(Guid id);
        public Task<List<Sprint>> GetAllAsync();

        public Task<List<Sprint>> GetProjectSprint(Guid projectId);
        public Task<TaskP> AddTaskToSprint(Guid sprintId, Guid taskId);
        public Task<TaskP> RemoveTaskFromSprint(Guid taskId);
        Task RemoveSprintDate(Guid sprintId);

        public Task<Sprint> GetByNameAsync(string sprintName);
        Task UpdateSprintDate(Guid sprintId, DateTime startDate, DateTime endDate);
    }
}