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
            return Ok(sprintsBelongToProject);
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
    }
}