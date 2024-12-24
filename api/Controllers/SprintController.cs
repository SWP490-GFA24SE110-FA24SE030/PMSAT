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

        [HttpGet]
        public async Task<IActionResult> GetAllSprint() 
        {
            var sprint = await _sprintRepo.GetAllAsync();
            var sprintDto = sprint.Select(s => s.ToSprintDto());

            return Ok(sprintDto);
        }

        [HttpGet("{sprintId}")]
        public async Task<IActionResult> GetSprintById([FromRoute] Guid sprintId)
        {
            var sprint = await _sprintRepo.GetByIdAsync(sprintId);
            return Ok(sprint);
        }

        [HttpGet("{projectId}/getAllSprintByProjectId")]
        public async Task<IActionResult> GetProjectPrint([FromRoute] Guid projectId)
        {
            var sprintsBelongToProject = await _sprintRepo.GetProjectSprint(projectId);
            return Ok(sprintsBelongToProject.Select(s => s.ToSprintDto()));
        }

        [HttpPut("sprintId = {sprintId}/taskId = {taskId}/AddTaskToSprint")]
        public async Task<IActionResult> AddTaskToSprint([FromRoute] Guid sprintId, [FromRoute] Guid taskId)
        {
            var task = await _taskRepo.GetByIdAsync(taskId);
            if (task == null) 
            {
                return BadRequest("Task does not exist!");
            }
            if (await _sprintRepo.GetByIdAsync(sprintId) == null) 
            {
                return BadRequest("Task does not exist!");
            }
            if (task.SprintId == sprintId)
            {
                return BadRequest("This task is already assigned to the ongoing sprint.");
            }
            await _sprintRepo.AddTaskToSprint(sprintId, taskId);
            return Ok("Add task successfuly!");
        }

        [HttpPut("RemoveTask/taskId = {taskId}")]
        public async Task<IActionResult> RemoveTaskFromSprint([FromRoute] Guid taskId)
        {
            var task = await _sprintRepo.RemoveTaskFromSprint(taskId);
            if (task == null) 
            {
                return BadRequest("Task does not exist!");
            }
            
            if (task.SprintId == Guid.Empty)
            {
                return BadRequest("This task is not assign to any sprint.");
            }
            
            return Ok("Remove task successfuly!");
        }

        [HttpDelete("{sprintId}/DeleteById")]
        public async Task<IActionResult> DeleteSprintById([FromRoute] Guid sprintId)
        {
            if (await _sprintRepo.GetByIdAsync(sprintId) == null)
            {
                return BadRequest("Sprint does not exist");
            }
            await _sprintRepo.DeleteByIdAsync(sprintId);
            return Ok("Delete Successfuly");
        }


        [HttpPost("{projectId}")]
        public async Task<IActionResult> Create([FromRoute] Guid projectId, CreateSprintRequest request)
        {
            if(!await _projectRepo.ProjectExist(projectId))
            {
                return BadRequest("Project is not exist!");
            }

            var sprintModel = request.ToSprintFromCreate(projectId);

            await _sprintRepo.CreateAsync(sprintModel);

            return Ok(sprintModel);
        }
    }
}