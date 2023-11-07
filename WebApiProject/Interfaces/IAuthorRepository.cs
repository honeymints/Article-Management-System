using WebApiProject.Models;

namespace WebApiProject.Interfaces;

public interface IAuthorRepository
{
    public ICollection<Author> GetAuthors();
    public ICollection<Article> GetArticleByAuthor(int id);
    public Author GetAuthor(int id);
    public bool CreateAuthor(Author author);
    public bool Save();
}