using WebApiProject.Data;
using WebApiProject.Interfaces;
using WebApiProject.Models;

namespace WebApiProject.Repository;

public class AuthorRepository : IAuthorRepository
{
    private readonly ApplicationDbContext _dbContext;
    public AuthorRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public ICollection<Article> GetArticleByAuthor(int id)
    {
        var article = _dbContext.AuthorArticles.Where(p => p.AuthorId == id).Select(e=>e.Article).ToList();
        return article;
    }

    public ICollection<Author> GetAuthors()
    {
        var authors = _dbContext.Authors.OrderBy(p=>p.Id).ToList();
        return authors;
    }

    public Author GetAuthor(int id)
    {
        var author = _dbContext.Authors.Where(p => p.Id == id).FirstOrDefault();
        return author;
    }

    public bool CreateAuthor(Author author)
    {
        _dbContext.Add(author);
        return Save();
    }

    public bool Save()
    {
        var saved = _dbContext.SaveChanges();
        return saved > 0 ? true : false;
    }
}