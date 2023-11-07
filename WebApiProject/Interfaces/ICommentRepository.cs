using WebApiProject.Models;

namespace WebApiProject.Interfaces;

public interface ICommentRepository
{
    public ICollection<Comment> GetComments();
    public Comment GetComment(int id);
    public bool CreateComment(Article article, User user, Comment comment);
    public bool UpdateComment(Comment comment);
    public bool Save();
}