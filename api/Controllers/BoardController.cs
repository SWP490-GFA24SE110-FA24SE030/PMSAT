﻿using api.Dtos.Board;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/board")]
    public class BoardController : ControllerBase
    {
        private readonly IBoardRepository _boardRepo;

        public BoardController(IBoardRepository boardRepo)
        {
            _boardRepo = boardRepo;
        }

        [HttpGet("prjid={projectId}/all")]
        public async Task<IActionResult> GetBoardsByProject([FromRoute] Guid projectId)
        {
            var boards = await _boardRepo.GetByProjectIdAsync(projectId);
            var boardDto = boards.Select(b => b.ToBoardDto());
            return Ok(boardDto);
        }

        [HttpPost("prjid={projectId}/new")]
        public async Task<IActionResult> CreateBoard([FromRoute] Guid projectId, [FromBody] CreateBoardDto boardDto)
        {
            var board = new Board
            {
                Id = Guid.NewGuid(),
                Status = boardDto.Status,
                ProjectId = projectId
            };

            var createdBoard = await _boardRepo.CreateAsync(board);
            return Ok(new { Message = "Board(s) created successfully." });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBoard([FromRoute] Guid id, [FromBody] UpdateBoardDto updateDto)
        {
            var updatedBoard = await _boardRepo.UpdateAsync(id, updateDto);

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
