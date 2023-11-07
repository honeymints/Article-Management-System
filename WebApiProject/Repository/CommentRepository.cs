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

    public bool CreateComment(Article article, User user, Comment comment)
    {
        comment.User = user;
        comment.Article = article;
        _dbContext.Add(comment);
        return Save();
    }

    public bool UpdateComment(Comment comment)
    {
        throw new NotImplementedException();
    }

    public bool Save()
    {
        var saved = _dbContext.SaveChanges();
        return saved > 0 ? true : false;
    }
}