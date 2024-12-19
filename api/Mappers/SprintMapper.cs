using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Sprint;
using api.Models;

namespace api.Mappers
{
    public static class SprintMapper
    {

        public static SprintDto ToSprintDto(this Sprint sprintModel) 
        {
            return new SprintDto
            {
                Id = sprintModel.Id,
                Name = sprintModel.Name,
                StartDate = sprintModel.StartDate,
                EndDate = sprintModel.EndDate,
            };
        }

        public static Sprint ToSprintFromCreate(this CreateSprintRequest request, Guid projectId) 
        {
            return new Sprint
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                ProjectId = projectId,
            };
        }
    }
}