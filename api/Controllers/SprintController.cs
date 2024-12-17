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
        private readonly ITaskSprintRepository _taskSprintRepo;
        public SprintController(ISprintRepository sprintRepo, IProjectRepository projectRepo, ITaskRepository taskRepo, ITaskSprintRepository taskSprintRepo)
        {
            _sprintRepo = sprintRepo;
            _projectRepo = projectRepo;
            _taskRepo = taskRepo;
            _taskSprintRepo = taskSprintRepo;
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
            return Ok(sprintsBelongToProject);
        }

        [HttpGet("{sprintId}/getSprintTaskById")]
        public async Task<IActionResult> GetSprintTask([FromRoute] Guid sprintId) 
        {
            var sprintModel = await _sprintRepo.GetByIdAsync(sprintId);
            var taskBelongToSprint = await _taskSprintRepo.GetSprintTask(sprintModel);
            return Ok(taskBelongToSprint);
        }

        [HttpGet("{sprintName}/getSprintTaskByName")]
        public async Task<IActionResult> GetSprintTask([FromRoute] String sprintName) 
        {
            var sprintModel = await _sprintRepo.GetByNameAsync(sprintName);
            var taskBelongToSprint = await _taskSprintRepo.GetSprintTask(sprintModel);
            return Ok(taskBelongToSprint);
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
            
            
            if (await _taskSprintRepo.TaskExistInSprint(task, sprintId) != null)
                return BadRequest("Task is already assigned to this sprint");
            
            var taskSprintModel = new TaskSprint
            {
                Id = Guid.NewGuid(),
                SprintId = sprint.Id,
                TaskId = taskId
            };

            await _taskSprintRepo.Create(taskSprintModel);

            return Ok(taskSprintModel);
            
        }   

        [HttpDelete("RemoveTaskFromSprint")]
        public async Task<IActionResult> RemoveTaskFromSprint(Guid sprintId, Guid taskId)
        {
            var sprint = await _sprintRepo.GetByIdAsync(sprintId);
            var taskModel = await _taskRepo.GetByIdAsync(taskId);
            if (sprint == null)
                return BadRequest("Sprint not found");

            if (await _taskSprintRepo.TaskExistInSprint(taskModel, sprintId) == null)
                return BadRequest("Task is not assigned to this sprint");
                
            
            await _taskSprintRepo.RemoveTask(taskModel, sprintId);
            
            return Ok(taskModel);
        }    



        
        
    }
}