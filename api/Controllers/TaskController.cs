using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Task;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/Task")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _taskRepo;
        private readonly IProjectRepository _projectRepo;
        private readonly IBoardRepository _boardRepo;

        public TaskController(ITaskRepository taskRepo, IProjectRepository projectRepo, IBoardRepository boardRepo)
        {
            _taskRepo = taskRepo;
            _projectRepo = projectRepo;
            _boardRepo = boardRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await _taskRepo.GetAllAsync();

            var taskDto = tasks.Select(s => s.ToTaskDto());

            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var task = await _taskRepo.GetByIdAsync(id);

            if (task == null) 
            {
                return NotFound();
            }

            return Ok(task);

        }

        [HttpGet("getSprintIdFromTaskId/taskId={taskId}")]
        public async Task<IActionResult> GetIdSprintFromTask([FromRoute] Guid taskId)
        {
            var task = await _taskRepo.GetByIdAsync(taskId);
            if (task == null) 
            {
                return NotFound();
            }
            var sprintId = task.SprintId;
            return Ok(sprintId);
        }

        [HttpGet("getTasksFromProject/prjid={projectId}")]
        public async Task<IActionResult> GetTasksFromProject([FromRoute] Guid projectId)
        {
            try
            {
                var tasks = await _taskRepo.GetTasksFromProjectAsync(projectId);

                // Convert the tasks to DTOs if needed before returning (optional)
                var taskDtos = tasks.Select(t => t.ToTaskDto()).ToList();

                return Ok(tasks);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving tasks.", error = ex.Message });
            }
        }

        [HttpPost("{projectId}")]
        public async Task<IActionResult> Create([FromRoute] Guid projectId, CreateTaskDto taskDto)
        {
            if(!await _projectRepo.ProjectExist(projectId))
            {
                return BadRequest("Project does not exist!");
            }

            var status = await _boardRepo.GetFirstBoardStatusByProjectIdAsync(projectId);

            // Create the Task entity from the DTO
            var taskModel = taskDto.ToTaskFromCreate(projectId,status);

            // Save the Task entity
            await _taskRepo.CreateAsync(taskModel);

            //return CreatedAtAction(nameof(GetById), new {id = taskModel}, taskModel.ToTaskDto());
            return Ok(new { Message = "Task created successfully." });
        }

        //[HttpPut("{taskId}")]
        //public async Task<IActionResult> Update([FromRoute] Guid taskId, [FromBody] UpdateTaskDto taskDto)
        //{
        //    if (taskDto.StartDate > taskDto.EndDate)
        //    {
        //        return BadRequest("End date must be greater than or equal to start date.");
        //    }

        //    if (taskDto.StartDate < DateTime.Now)
        //    {
        //        return BadRequest("Start date cannot be in the past.");
        //    }

        //    // Check if the task exists
        //    var existingTask = await _taskRepo.GetByIdAsync(taskId);
        //    if (existingTask == null)
        //    {
        //        return NotFound("Task not found.");
        //    }

        //    // Update task properties
        //    existingTask.Status = taskDto.Status;
        //    existingTask.Name = taskDto.Name;
        //    existingTask.Description = taskDto.Description;
        //    existingTask.Priority = taskDto.Priority;
        //    existingTask.StartDate = taskDto.StartDate;
        //    existingTask.EndDate = taskDto.EndDate;

        //    await _taskRepo.UpdateAsync(existingTask);

        //    // Get the current status from the latest workflow entry for this task
        //    var latestWorkflow = await _workflowRepo.GetLatestWorkflowForTaskAsync(existingTask.Id);

        //    // Save workflow history
        //    var newWorkflow = new Workflow
        //    {
        //        Id = Guid.NewGuid(),
        //        TaskId = existingTask.Id,
        //        OldStatus = latestWorkflow?.CurrentStatus ?? "To-Do", // Default to "To-Do" if no prior status
        //        CurrentStatus = taskDto.Status,
        //        NewStatus = taskDto.Status,
        //        UpdatedAt = DateTime.Now
        //    };
        //    await _workflowRepo.CreateAsync(newWorkflow);
        //    return Ok(new { Message = "Task updated successfully."});
        //}

        [HttpPut("tsk={taskId}/updateStatus")]
        public async Task<IActionResult> UpdateTaskStatus([FromRoute] Guid taskId, [FromBody] string status)
        {
            var task = await _taskRepo.GetByIdAsync(taskId);
            if(task == null)
            {
                return NotFound();
            }
            var statusModel =  await _taskRepo.UpdateTaskStatusAsync(taskId, status);
            return Ok(statusModel);
        }

        [HttpPut("tsk={taskId}/changeBoard/brd={boardId}")]
        public async Task<IActionResult> ChangeBoard([FromRoute] Guid taskId, [FromRoute] Guid boardId)
        {
            // Check if the task exists
            var task = await _taskRepo.GetByIdAsync(taskId);
            if (task == null)
            {
                return NotFound($"Task with ID {taskId} not found.");
            }

            // Check if the board exists
            var board = await _boardRepo.GetByIdAsync(boardId);
            if (board == null)
            {
                return NotFound($"Board with ID {boardId} not found.");
            }

            var status = board.Status;
            await _taskRepo.ChangeBoard(taskId,boardId);
            await _taskRepo.UpdateTaskStatusAsync(taskId,status);
            return Ok(new { Message = "sucess" });
        }

        [HttpPost("LeaderID={leaderId}/assign/TaskID={taskId}")]
        public async Task<IActionResult> AssignTaskToMember([FromRoute] Guid leaderId, [FromRoute] Guid taskId, [FromBody] AssignTaskToMemberDto taskAssignment)
        {
            var resultMessage = await _taskRepo.AssignTaskToMemberAsync(leaderId, taskId, taskAssignment.Email);

            if (resultMessage == "Success")
            {
                return Ok(new { message = "Task assigned successfully." });
            }
            return BadRequest(new { message = resultMessage });
        }

        [HttpDelete]
        [Route("delete/tskid={id}")]
        public async Task<IActionResult> DeleteByIdAsync([FromRoute] Guid id)
        {
            var taskModel = await _taskRepo.DeleteByIdAsync(id);

            return Ok(new { Message = "Task(s) deleted successfully." });
        }
    }
}