using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Sprint;
using api.Models;
using api.Utils;

namespace api.Interfaces
{
    public interface ITaskSprintRepository
    {
        public Task<List<TaskP>> GetSprintTask(Sprint sprint);
        public Task<TaskSprint> Create(TaskSprint taskSprint);
        public Task<TaskSprint> RemoveTask(TaskP task, Guid sprintId);


    }

}