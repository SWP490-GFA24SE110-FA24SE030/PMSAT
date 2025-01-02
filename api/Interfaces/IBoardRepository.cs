using api.Dtos.Board;
using api.Models;

namespace api.Interfaces
{
    public interface IBoardRepository
    {
        Task<Board> CreateAsync(Board board);
        Task<List<Board>> GetAllAsync();
        Task AddTaskToBoard(Guid taskId, Guid boardId);
        Task<List<BoardResponse>> GetByProjectIdAsync(Guid projectId);
        Task<Board> GetByIdAsync(Guid id);
        Task<string> GetFirstBoardStatusByProjectIdAsync(Guid projectId);
        Task<Board?> UpdateAsync(Guid id, string status);
        Task<bool> DeleteAsync(Guid id);
        
    }
}
