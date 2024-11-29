using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Sprint;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SprintController : ControllerBase
    {
        private readonly ISprintRepository _sprintRepo;
        private readonly IProjectRepository _projectRepo;
        private readonly ITaskRepository _taskRepo;
        public SprintController(ISprintRepository sprintRepo, IProjectRepository projectRepo, ITaskRepository taskRepo)
        {
            _sprintRepo = sprintRepo;
            _projectRepo = projectRepo;
            _taskRepo = taskRepo;
        }

        [HttpPost("{projectId}")]
        public async Task<IActionResult> Create([FromRoute] Guid projectId, CreateSprintRequest request)
        {
            if(!await _projectRepo.ProjectExist(projectId))
            {
                return BadRequest("Project does not exist!");
            }

            var sprintModel = request.ToSprintFromCreate(projectId);

            await _sprintRepo.CreateAsync(sprintModel);

            return Ok(sprintModel);
        }

        [HttpPost("{sprintId}/tasks/{taskId}")]
        public async Task<IActionResult> AddTaskToSprint([FromRoute] Guid sprintId,[FromRoute] Guid taskId) 
        {
            var sprint = await _sprintRepo.GetByIdAsync(sprintId);
            if (sprint == null)
                return BadRequest("Sprint does not exist!");


            if (sprint.StartDate == null || sprint.EndDate == null)
                return BadRequest("Sprint must have start and end dates");

            var task = await _taskRepo.GetByIdAsync(taskId);
            if (task == null)
                return BadRequest("Task not found");
            
            
            sprint.TaskSprints.Add(new TaskSprint
            {
                Id = Guid.NewGuid(),
                SprintId = sprintId,
                TaskId = taskId
            });

            return NoContent();
            
        }       



        
        
    }
}