using Api.Dtos.CommentDtos;
using Api.Entities;
using Api.Helpers;

namespace Api.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync(CommentQuery query);
        Task<Comment?> GetByIdAsync(int id);
        Task<Comment> CreateAsync(Comment comment);
        Task<Comment?> UpdateAsync(int id, UpdateCommentDto commentDto);
        Task<Comment?> DeleteAsync(int id);
    }
}
