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
                Status = taskModel.Status,
                Description = taskModel.Description,
                StartDate = taskModel.StartDate,
                EndDate = taskModel.EndDate,
                
            };
        }

        public static TaskP ToTaskFromCreate(this CreateTaskDto taskDto, Guid projectId) 
        {
            return new TaskP
            {
                Id = Guid.NewGuid(),
                Status = taskDto.Status,
                Description = taskDto.Description,
                StartDate = taskDto.StartDate,
                EndDate = taskDto.EndDate,
                ProjectId = projectId
            };
        }
    }
}