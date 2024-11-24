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
        Task<TaskP> CreateAsync(TaskP taskModel);
        Task<string> AssignTaskToMemberAsync(Guid leaderId, Guid taskId, string email);
    }
}