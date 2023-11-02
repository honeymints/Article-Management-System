using WebApiProject.Models;

namespace WebApiProject.Interfaces;

public interface IArticleRepository
{
    public ICollection<Article> GetArticles();
    public Article GetArticle(int id);
    public Article GetArticle(string name);
    public bool ArticleExists(int id);

}