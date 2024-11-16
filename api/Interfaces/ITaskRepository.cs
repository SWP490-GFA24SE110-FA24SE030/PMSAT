using System;
using System.Collections.Generic;
using System.Linq;
using api.Models;



namespace api.Interfaces
{
    public interface ITaskRepository
    {
        Task<List<TaskP>> GetAllAsync();
        Task<TaskP> GetByIdAsync(Guid id);
        Task<TaskP> CreateAsync(TaskP taskModel);
    }
}