using api.Dtos.Board;
using api.Models;

namespace api.Interfaces
{
    public interface IBoardRepository
    {
        Task<Board> CreateAsync(Board board);
        Task<List<BoardResponse>> GetByProjectIdAsync(Guid projectId);
        Task<Board> GetByIdAsync(Guid id);
        Task<string> GetFirstBoardStatusByProjectIdAsync(Guid projectId);
        Task<Board?> UpdateAsync(Guid id, UpdateBoardDto updateDto);
        Task<bool> DeleteAsync(Guid id);
    }
}
