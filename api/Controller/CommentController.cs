using api.Data;
using api.DTO.Comment;
using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController(ICommentRepository repository) : ControllerBase
    {
        private readonly ICommentRepository _repository = repository;

        [HttpGet]
        [Route("GetComments")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetComments()
        {
            var comments = await _repository.GetComments();
            return Ok(comments);
        }

        [HttpGet]
        [Route("GetComment/{id:int}")]
        public async Task<ActionResult<Comment>> GetComment([FromRoute] int id)
        {
            var comment = await _repository.GetComment(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment);
        }

        [HttpPost]
        [Route("CreateComment")]
        public async Task<ActionResult<Comment>> CreateComment([FromBody] CreateCommentDto comment)
        {
            if (comment == null)
            {
                return BadRequest();
            }
            Comment savedComment = await _repository.CreateComment(comment);

            return CreatedAtAction(nameof(GetComment), new { id = savedComment.Id }, comment);
        }

        [HttpPut]
        [Route("UpdateComment/{id:int}")]
        public async Task<IActionResult> UpdateComment([FromRoute] int id, [FromBody] UpdateCommentDto comment)
        {
            if (comment == null)
            {
                return BadRequest();
            }

            await _repository.UpdateComment(id, comment);

            return NoContent();
        }

        [HttpDelete]
        [Route("DeleteComment/{id:int}")]
        public async Task<IActionResult> DeleteComment([FromRoute] int id)
        {
            var comment = await _repository.DeleteComment(id);
            if (comment == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}