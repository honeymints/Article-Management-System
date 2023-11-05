using WebApiProject.Models;

namespace WebApiProject.Interfaces;

public interface IUserRepository
{
    public ICollection<User> GetUsers();
    public User GetUser(int id);
    public List<Comment> GetCommentsByUser(int id);

}