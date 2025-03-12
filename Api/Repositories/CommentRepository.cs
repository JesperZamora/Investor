using System.Linq.Expressions;
using Api.Data;
using Api.Dtos.CommentDtos;
using Api.Entities;
using Api.Helpers;
using Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext _context;

        public CommentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Comment>> GetAllAsync(CommentQuery query)
        {
            var comments = _context.Comments.AsQueryable();
            
            var (sortby, decend, pageSize, pageNumber) = query;

            // Sorting
            if (!string.IsNullOrEmpty(sortby) && HasProperty.Check<Comment>(sortby, out string? propertyName))
            {
                comments = comments.CustomOrderBy<Comment>(propertyName, decend);
            }
            else
            {
                comments = comments.OrderBy(c => c.Id);
            }

            // Pagination
            var skipNumber = (pageNumber - 1) * pageSize;

            return await comments.Skip(skipNumber).Take(pageSize).ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Comment> CreateAsync(Comment newComment)
        {
            _context.Comments.Add(newComment);
            await _context.SaveChangesAsync();
            return newComment;
        }

        public async Task<Comment?> UpdateAsync(int id, UpdateCommentDto commentDto)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);

            if (comment is null) return comment;

            comment.Title = commentDto.Title;
            comment.Content = commentDto.Content;

            await _context.SaveChangesAsync();

            return comment;
        }

        public async Task<Comment?> DeleteAsync(int id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);

            if (comment is null) return comment;

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return comment;
        }
    }
}
