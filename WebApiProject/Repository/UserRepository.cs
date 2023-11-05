using Microsoft.EntityFrameworkCore;
using WebApiProject.Data;
using WebApiProject.Interfaces;
using WebApiProject.Models;

namespace WebApiProject.Repository;


public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;
    public UserRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public ICollection<User> GetUsers()
    {
        var users = _dbContext.Users.ToList();
        return users;
    }

    public User GetUser(int id)
    {
        var user = _dbContext.Users.Where(u => u.Id == id).Include(u=>u.Comments).FirstOrDefault();
        return user;
    }

    public List<Comment> GetCommentsByUser(int id)
    {
        var comments = _dbContext.Comments.Where(u=>u.User.Id==id).ToList();
        return comments;
    }
}