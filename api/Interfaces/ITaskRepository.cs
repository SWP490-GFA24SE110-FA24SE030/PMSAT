using System;
using System.Collections.Generic;
using System.Linq;
using api.Dtos.Task;
using api.Models;
using Microsoft.AspNetCore.Mvc;



namespace api.Interfaces
{
    public interface ITaskRepository
    {
        Task<List<TaskP>> GetAllAsync();
        Task<TaskP> GetByIdAsync(Guid id);
        Task<List<TaskP>> GetTasksFromProjectAsync(Guid projectId);
        public Task<List<TaskP>> GetTasksFromSprintAsync(Guid sprintId);
        Task<TaskP> CreateAsync(TaskP taskModel);
        Task UpdateAsync(TaskP task);
        Task<string> UpdateTaskStatusAsync(Guid taskId, string status);
        Task<Board> ChangeBoard(Guid taskId, Guid boardId);
        Task<string> AssignTaskToMemberAsync(Guid leaderId, Guid taskId, string email);
        Task<TaskP> DeleteByIdAsync(Guid id);
    }
}