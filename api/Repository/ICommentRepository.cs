using api.DTO.Comment;
using api.Models;

namespace api.Repository
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetComments();
        Task<Comment> GetComment(int Id);
        Task<Comment> DeleteComment(int Id);
        Task<Comment> CreateComment(CreateCommentDto commentDto);

        Task<Comment> UpdateComment(int Id, CreateCommentDto commentDto);
    }
}