using WebApiProject.Models;

namespace WebApiProject.Interfaces;

public interface ICommentRepository
{
    public ICollection<Comment> GetComments();
    public Comment GetComment(int id);
}