using WebApiProject.Data;
using WebApiProject.Interfaces;
using WebApiProject.Models;

namespace WebApiProject.Repository;

public class ArticleRepository : IArticleRepository
{
    private readonly ApplicationDbContext _context;

    public ArticleRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Article GetArticle(string name)
    {
        var articleByName = _context.Articles.FirstOrDefault(p => p.Title == name);
        return articleByName;
    }
    
    public Article GetArticle(int id)
    {
        var aritlceById = _context.Articles.FirstOrDefault(p => p.Id == id);
        return aritlceById;
    }

    public bool ArticleExists(int id)
    {
        return _context.Articles.Any(p => p.Id == id);
    }

    public ICollection<Article> GetArticles()
    {
        return _context.Articles.OrderBy(a => a.Id).ToList();
    }
    
    public ICollection<Comment> GetCommentsByArticle(int id)
    {
        var comments = _context.Comments.Where(c=>c.Article.Id==id).ToList();
        return comments;
        //var comments=_dbContext.Comments.Where(c=>c.Id==id).Select(a=>a.Article)
    }

    public bool CreateArticle(Author author, Category category, Article article)
    {
        AuthorArticles authorArticles = new()
        {
            Author = author,
            Article = article
        };
        _context.Add(authorArticles);
        
        CategoryArticles categoryArticles = new()
        {
            Article = article,
            Category = category
        };
        article.CategoriesList = new List<CategoryArticles>()
        {
            categoryArticles
        };
        //  Object reference not set to an instance of an object.
        _context.Add(categoryArticles);
        
        _context.Add(article);
        return Save();
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }
}