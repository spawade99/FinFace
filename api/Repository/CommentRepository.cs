using api.Data;
using api.DTO.Comment;
using api.Models;
using api.Mapper;
using Microsoft.EntityFrameworkCore;

namespace api.Repository;

public class CommentRepository(ApplicationDBContext dbContext, IStockRepository stockRepository) : ICommentRepository
{
    private readonly ApplicationDBContext _context = dbContext;
    private readonly IStockRepository _stockRepository = stockRepository;

    public async Task<Comment> CreateComment(CreateCommentDto commentDto)
    {
        if (commentDto.StockId.HasValue && await _stockRepository.IsStockExists(commentDto.StockId.Value))
        {
            Comment comment = commentDto.ToComment();
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return comment;
        }
        else
            throw new Exception("Invalid stock ID or stock does not exist");
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

    public async Task<Comment> UpdateComment(int Id, UpdateCommentDto commentDto)
    {
        Comment? comment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == Id);
        if (comment == null)
        {
            throw new Exception("Comment not found");
        }

        comment.Title = commentDto.Title;
        comment.Content = commentDto.Content;

        _context.Comments.Update(comment);
        await _context.SaveChangesAsync();
        return comment;
    }
}