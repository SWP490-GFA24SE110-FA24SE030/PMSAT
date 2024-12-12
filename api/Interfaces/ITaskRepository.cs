using System;
using System.Collections.Generic;
using System.Linq;
using api.Dtos.Task;
using api.Models;



namespace api.Interfaces
{
    public interface ITaskRepository
    {
        Task<List<TaskP>> GetAllAsync();
        Task<TaskP> GetByIdAsync(Guid id);
        Task<List<TaskP>> GetTasksFromProjectAsync(Guid projectId);
        Task<TaskP> CreateAsync(TaskP taskModel);
        Task UpdateAsync(TaskP task);
        Task<string> AssignTaskToMemberAsync(Guid leaderId, Guid taskId, string email);
        Task<TaskP> DeleteByIdAsync(Guid id);
    }
}