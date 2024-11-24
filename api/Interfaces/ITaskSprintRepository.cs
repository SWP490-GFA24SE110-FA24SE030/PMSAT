using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Sprint;
using api.Utils;

namespace api.Interfaces
{
    public interface ITaskSprintRepository
    {
        public Task<Guid> CreateSprint(CreateSprintRequest request);
        public Task<Guid> AddTaskToSprint(Guid taskId, Guid sprintId);


    }

}