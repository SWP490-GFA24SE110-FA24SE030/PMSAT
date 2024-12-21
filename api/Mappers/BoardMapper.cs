using api.Dtos.Board;
using api.Models;

namespace api.Mappers
{
    public static class BoardMapper
    {
        public static BoardResponse ToBoardDto (this Board boardModel)
        {
            return new BoardResponse
            {
                Status = boardModel.Status,
            };
        }
    }
}
