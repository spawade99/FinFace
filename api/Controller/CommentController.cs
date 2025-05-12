using api.Data;
using api.DTO.Comment;
using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controller
{
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

        [HttpPost]
        [Route("CreateComment")]
        public async Task<ActionResult<Comment>> CreateComment([FromBody] CreateCommentDto comment)
        {
            if (comment == null)
            {
                return BadRequest();
            }
            Comment savedComment = await _repository.CreateComment(comment);

            return CreatedAtAction(nameof(GetComments), new { id = savedComment.Id }, comment);
        }

        [HttpPut]
        [Route("UpdateComment/{id}")]
        public async Task<IActionResult> UpdateComment([FromRoute] int id, [FromBody] CreateCommentDto comment)
        {
            if (comment == null)
            {
                return BadRequest();
            }

            await _repository.UpdateComment(id, comment);

            return NoContent();
        }
    }
}