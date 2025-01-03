using api.Dtos.Board;
using api.Dtos.Task;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace api.Repository
{
    public class BoardRepository : IBoardRepository
    {
        private readonly PmsatContext _context;

        public BoardRepository(PmsatContext context)
        {
            _context = context;
        }

        public async Task<Board> CreateAsync(Board board)
        {
            await _context.Boards.AddAsync(board);
            await _context.SaveChangesAsync();
            return board;
        }

        public async Task<List<Board>> GetAllAsync()
        {
            return await _context.Boards.ToListAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var board = await _context.Boards
                .Include(b => b.TaskPs)  
                .FirstOrDefaultAsync(b => b.Id == id);

            if (board == null)
                return false;

            // Set BoardId to null for all related TaskPs
            foreach (var task in board.TaskPs)
            {
                task.BoardId = null;
                task.Status = null;
                task.Updated = DateTime.Now;
            }

            _context.Boards.Remove(board);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<string> GetFirstBoardStatusByProjectIdAsync(Guid projectId)
        {
            var board = await _context.Boards
                        .FirstOrDefaultAsync(b => b.ProjectId == projectId && b.Orders == 1);
            var status = board.Status;
            return status;
        }

        public async Task<List<BoardResponse>> GetByProjectIdAsync(Guid projectId)
        {
            return await _context.Boards
                .Where(b => b.ProjectId == projectId)
                .OrderBy(b => b.Orders)
                .Include(b => b.TaskPs)
                .Select(b => new BoardResponse
                {
                    Id = b.Id,
                    Status = b.Status, // status of Board 
                    TaskPs = b.TaskPs.Select(t => new TaskDto
                    {
                        Id = t.Id,
                        Title = t.Title,
                        Description = t.Description,
                        Priority = t.Priority,
                        Created = t.Created,
                        Updated = t.Updated,
                        Status = t.Status // status of Task
                    }).ToList(),
                    Orders = b.Orders
                })
                .ToListAsync();
        }


        public async Task<Board?> UpdateAsync(Guid id, string status)
        {
            var board = await _context.Boards.FindAsync(id);

            if (board == null)
                return null;

            board.Status = status;

            await _context.SaveChangesAsync();
            return board;
        }

        public async Task<Board> GetByIdAsync(Guid id)
        {
            return await _context.Boards.FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task AddTaskToBoard(Guid taskId, Guid boardId)
        {
            var board = await _context.Boards
                        .Include(b => b.TaskPs)
                        .FirstOrDefaultAsync(b => b.Id == boardId);

            if (board == null)
            {
                throw new Exception("Board not found");
            }

            // Check if the task with the given taskId already exists
            var existingTask = await _context.TaskPs
                               .FirstOrDefaultAsync(t => t.Id == taskId);

            if (existingTask == null)
            {
                throw new Exception("Task not found.");
            }

            // Check if the task is already added to the board
            if (board.TaskPs.Any(t => t.Id == taskId))
            {
                throw new Exception("This task is already added to the board.");
            }

            existingTask.Updated = DateTime.Now;
            existingTask.Status = board.Status; // get status of Board insert into status of Task
            existingTask.BoardId = boardId;

            board.TaskPs.Add(existingTask);
            await _context.SaveChangesAsync();
        }
    }
}
