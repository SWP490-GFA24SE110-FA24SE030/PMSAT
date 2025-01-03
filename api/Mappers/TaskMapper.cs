using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Task;
using api.Models;

namespace api.Mappers
{
    public static class TaskMapper
    {
        public static TaskDto ToTaskDto(this TaskP taskModel) 
        {
            return new TaskDto
            {
                Id = taskModel.Id,
                Title = taskModel.Title,
                Description = taskModel.Description,
                Priority = taskModel.Priority,
                Created = taskModel.Created,
                Updated = taskModel.Updated,
                Status = taskModel.Status,
                Tags = taskModel.Tags.Select(t => t.ToTagDto()).ToList(),
            };
        }

        public static TaskP ToTaskFromCreate(this CreateTaskDto taskDto, Guid projectId, string status) 
        {
            return new TaskP
            {
                Id = Guid.NewGuid(),
                Title = taskDto.Title,
                Description = "",
                Priority = 0,
                Created = DateTime.Now,
                Updated = DateTime.Now,
                Status = status,
                ProjectId = projectId
            };
        }
    }
}