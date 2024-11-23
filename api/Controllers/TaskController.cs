using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Task;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/Task")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _taskRepo;
        private readonly IProjectRepository _projectRepo;
        private readonly IWorkFlowRepository _workflowRepo;

        public TaskController(ITaskRepository taskRepo, IProjectRepository projectRepo, IWorkFlowRepository workflowRepo)
        {
            _taskRepo = taskRepo;
            _projectRepo = projectRepo;
            _workflowRepo = workflowRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await _taskRepo.GetAllAsync();

            var taskDto = tasks.Select(s => s.ToTaskDto());

            return Ok(taskDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var task = await _taskRepo.GetByIdAsync(id);

            if (task == null) 
            {
                return NotFound();
            }

            return Ok(task.ToTaskDto());

        }

        [HttpPost("{projectId}")]
        public async Task<IActionResult> Create([FromRoute] Guid projectId, CreateTaskDto taskDto)
        {
            if(!await _projectRepo.ProjectExist(projectId))
            {
                return BadRequest("Project does not exist!");
            }

            // Create the Task entity from the DTO
            var taskModel = taskDto.ToTaskFromCreate(projectId);

            // Save the Task entity
            await _taskRepo.CreateAsync(taskModel);

            // Create an initial Workflow entry for the task
            var workflowModel = new Workflow
            {
                Id = Guid.NewGuid(),
                OldStatus = "default",
                CurrentStatus = "To-Do",
                NewStatus = "To-Do",
                UpdatedAt = DateTime.Now,
                TaskId = taskModel.Id,
            };

            // Save the Workflow entity
            await _workflowRepo.CreateAsync(workflowModel);

            return CreatedAtAction(nameof(GetById), new {id = taskModel}, taskModel.ToTaskDto());
        }

        [HttpPut("{taskId}/status")]
        public async Task<IActionResult> EditTaskStatus([FromRoute] Guid taskId, [FromBody] UpdateTaskStatusDto statusDto)
        {
            // Check if the task exists
            var task = await _taskRepo.GetByIdAsync(taskId);
            if (task == null)
            {
                return NotFound("Task not found.");
            }

            // Get the current status from the latest workflow entry for this task
            var latestWorkflow = await _workflowRepo.GetLatestWorkflowForTaskAsync(task.Id);

            // Save workflow history
            var newWorkflow = new Workflow
            {
                Id = Guid.NewGuid(),
                TaskId = task.Id,
                OldStatus = latestWorkflow?.CurrentStatus ?? "To-Do", // Default to "To-Do" if no prior status
                CurrentStatus = statusDto.NewStatus,
                NewStatus = statusDto.NewStatus,
                UpdatedAt = DateTime.Now
            };
            await _workflowRepo.CreateAsync(newWorkflow);

            return Ok(new { Message = "Task status updated successfully.", Workflow = newWorkflow });
        }

        [HttpPost("assign")]
        public async Task<IActionResult> AssignTaskToMember([FromBody] AssignTaskToMemberDto taskAssignment)
        {
            if (taskAssignment == null)
            {
                return BadRequest("Invalid data.");
            }
            var resultMessage= await _taskRepo.AssignTaskToMemberAsync(taskAssignment);
            if (resultMessage == "Success")
            {
                return Ok(new { message = "Task assigned successfully." });
            }
            else
            {
                return BadRequest(new { message = resultMessage });
            }
        }
    }
}