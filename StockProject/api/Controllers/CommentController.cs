using api.Data;
using api.Dtos.Comment;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("/api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;

        public CommentController(ICommentRepository commentRepo)
        {
            _commentRepo = commentRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Comment> comments = await _commentRepo.GetAllASync();

            return Ok(comments.Select(comment => comment.ToCommentDto()));
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetCommentById([FromRoute] int id)
        {
            Comment? comment = await _commentRepo.GetByIdAsync(id);
            return comment == null
                ? NotFound()
                : Ok(comment.ToCommentDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentRequestDto commentDto, [FromQuery] int stockId)
        {
            // Validate Model State
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Comment comment = await _commentRepo.CreateAsync(
                commentDto.ToCommentFromCreate(stockId)
            );
            
            return Ok(comment.ToCommentDto());
        }

    }
}