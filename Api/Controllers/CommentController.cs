using Api.Dtos.CommentDtos;
using Api.Mappers;
using Api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Api.Helpers;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;

        public CommentController(ICommentRepository commentRepo, IStockRepository stockRepo)
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] CommentQuery query)
        {
            var comments = await _commentRepo.GetAllAsync(query);

            var ToCommentDtos = comments.Select(c => c.ToCommentDto()).ToList();

            return Ok(ToCommentDtos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var comment = await _commentRepo.GetByIdAsync(id);

            return comment is null ? NotFound() : Ok(comment.ToCommentDto());
        }

        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, [FromBody] CreateCommentDto newComment)
        {
            if (!await _stockRepo.StockExistAsync(stockId))
            {
                return NotFound($"Stock with id {stockId}, not found.");
            }
            
            var comment = newComment.ToComment(stockId);

            var commentCreated = await _commentRepo.CreateAsync(comment);
            
            return CreatedAtAction(nameof(GetById), new { id = comment.Id }, commentCreated.ToCommentDto());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentDto updateComment)
        {
            var commentUpdated = await _commentRepo.UpdateAsync(id, updateComment);

            return commentUpdated is null ? NotFound() : Ok(commentUpdated.ToCommentDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var commentDeleted = await _commentRepo.DeleteAsync(id);

            return commentDeleted is null ? NotFound() : NoContent();
        }
    }
}
