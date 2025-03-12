using Api.Dtos.CommentDtos;
using Api.Entities;

namespace Api.Mappers
{
    public static class CommentMappers
    {
        public static CommentDto ToCommentDto(this Comment comment)
        {
            return new ()
            {
                Id = comment.Id,
                Title = comment.Title,
                Content = comment.Content,
                CreatedOn = comment.CreatedOn,
                StockId = comment.StockId
            };
        }

        public static Comment ToComment(this CreateCommentDto commentDto, int stockId)
        {
            return new ()
            {
                Title = commentDto.Title,
                Content = commentDto.Content,
                StockId = stockId
            };
        }
    }
}
