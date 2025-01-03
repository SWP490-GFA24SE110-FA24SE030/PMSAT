﻿using api.Dtos.Board;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController]
    [Route("api/board")]
    public class BoardController : ControllerBase
    {
        private readonly IBoardRepository _boardRepo;
        private readonly ITaskRepository _taskRepo;

        public BoardController(IBoardRepository boardRepo, ITaskRepository taskRepo)
        {
            _boardRepo = boardRepo;
            _taskRepo = taskRepo;
        }

        [HttpGet("prjid={projectId}/all")]
        public async Task<IActionResult> GetBoardsByProject([FromRoute] Guid projectId)
        {
            var boards = await _boardRepo.GetByProjectIdAsync(projectId);
            
            return Ok(boards);
        }

        [HttpPost("prjid={projectId}/new")]
        public async Task<IActionResult> CreateBoard([FromRoute] Guid projectId, [FromBody] CreateBoardDto boardDto)
        {
            var boards = await _boardRepo.GetByProjectIdAsync(projectId);
            var maxValue = boards.Max(b => b.Orders);
            maxValue++;

            var board = new Board
            {
                Id = Guid.NewGuid(),
                Status = boardDto.Status,
                ProjectId = projectId,
                Orders = maxValue,
            };

            var createdBoard = await _boardRepo.CreateAsync(board);
            return Ok(new { Message = "Board(s) created successfully." });
        }

        [HttpPut("board/{boardId}/task/{taskId}/AddTaskToBoard")]
        public async Task<IActionResult> AddTaskToBoardAsync(Guid boardId, Guid taskId)
        {
            try
            {
                await _boardRepo.AddTaskToBoard(taskId, boardId);
                return Ok("Task added to this Board successfully.");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}/{status}")]
        public async Task<IActionResult> UpdateBoard([FromRoute] Guid id, [FromRoute] string status)
        {
            var board = await _boardRepo.GetByIdAsync(id);
            var tasks = await _taskRepo.GetAllAsync();

            foreach (var task in tasks)
            {
                if (task.Status == board.Status && task.ProjectId == board.ProjectId)
                {
                    await _taskRepo.UpdateTaskStatusAsync(task.Id, status);
                }
            }
            var updatedBoard = await _boardRepo.UpdateAsync(id, status);
            
            if (updatedBoard == null)
                return NotFound();

            return Ok(new { Message = "Board(s) updated successfully." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBoard([FromRoute] Guid id)
        {
            var success = await _boardRepo.DeleteAsync(id);

            if (!success)
                return NotFound();

            return Ok(new { Message = "Board(s) deleted successfully." });
        }

    }
}
