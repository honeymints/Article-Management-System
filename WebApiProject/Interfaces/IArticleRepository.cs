using WebApiProject.Models;

namespace WebApiProject.Interfaces;

public interface IArticleRepository
{
    public ICollection<Article> GetArticles();
    public Article GetArticle(int id);
    public Article GetArticle(string name);
    public bool ArticleExists(int id);

    public ICollection<Comment> GetCommentsByArticle(int id);

    public bool CreateArticle(Author author, Category category, Article article);
    public bool Save();


}