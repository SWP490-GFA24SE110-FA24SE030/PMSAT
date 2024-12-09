using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Project;
using api.Models;

namespace api.Mappers
{
    public static class ProjectMappers
    {
        public static ProjectDto ToProjectDto(this Project projectModel)
        {
            return new ProjectDto
            {
                Id = projectModel.Id,
                Title = projectModel.Title,
                Description = projectModel.Description,
                CreatedAt = projectModel.CreatedAt,
                Status = projectModel.Status,
                TaskPs = projectModel.TaskPs.Select(t => t.ToTaskDto()).ToList(),
                Sprints = projectModel.Sprints.Select(s => s.ToSprintDto()).ToList()
            };
        }

        public static Project ToProjectFromCreateDto(this CreateProjectRequestDto projectDto)
        {
            return new Project 
            {
                Id = Guid.NewGuid(),
                Title = projectDto.Title,
                Description = projectDto.Description,
                CreatedAt = DateTime.Now,
                Status = "Active"
            };
        }
    }
}