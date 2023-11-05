using WebApiProject.Data;
using WebApiProject.Interfaces;
using WebApiProject.Models;

namespace WebApiProject.Repository;

public class CommentRepository : ICommentRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CommentRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public ICollection<Comment> GetComments()
    {
        return _dbContext.Comments.ToList();
    }

    public Comment GetComment(int id)
    {
        var comment = _dbContext.Comments.Where(u => u.Id == id).FirstOrDefault();
        return comment;
    }
    
}