using api.Data;
using api.DTO.Comment;
using api.Models;
using api.Mapper;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CommentRepository(ApplicationDBContext dbContext) : ICommentRepository
    {
        public readonly ApplicationDBContext _context = dbContext;

        public async Task<Comment> CreateComment(CreateCommentDto commentDto)
        {
            Comment comment = commentDto.ToComment();
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment> DeleteComment(int Id)
        {
            Comment? comment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == Id);
            if (comment == null)
            {
                throw new Exception("Comment not found");
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment> GetComment(int Id)
        {
            Comment? comment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == Id);
            if (comment == null)
            {
                throw new Exception("Comment not found");
            }
            return comment;
        }

        public async Task<IEnumerable<Comment>> GetComments()
        {
            var comments = await _context.Comments.ToListAsync();
            if (comments == null)
            {
                throw new Exception("No comments found");
            }

            return comments;
        }

        public async Task<Comment> UpdateComment(int Id, CreateCommentDto commentDto)
        {
            Comment? comment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == Id);
            if (comment == null)
            {
                throw new Exception("Comment not found");
            }

            comment.Title = commentDto.Title;
            comment.Content = commentDto.Content;
            comment.StockId = commentDto.StockId;
            comment.CreatedAt = DateTime.UtcNow;

            _context.Comments.Update(comment);
            await _context.SaveChangesAsync();
            return comment;
        }
    }
}