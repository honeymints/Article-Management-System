using System.Xml.Linq;
using WebApiProject.Data;
using WebApiProject.Interfaces;
using WebApiProject.Models;

namespace WebApiProject.Repository;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CategoryRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public List<Category> GetCategories()
    {
        var categories = _dbContext.Categories.ToList();
        return categories;
    }

    public bool CategoryExists(int id) //checks if certain category exists
    {
        return _dbContext.Categories.Any(p=>p.Id==id);
    }

    public bool CategoryExists(string categoryType)
    {
        return _dbContext.Categories.Any(c => c.CategoryType.Trim().ToLower() == categoryType.Trim().ToLower());
    }

    public ICollection<Article> GetArticlesByCategory(int id)
    {
        var articles = _dbContext.CategoryArticles.Where(p => p.CategoryId == id).Select(p => p.Article).ToList();
        return articles;
    }

    public ICollection<Article> GetArticlesByCategory(string type)
    {
        throw new NotImplementedException();
    }

    public bool CreateCategory(Category category)
    {
        _dbContext.Add(category);
        return Save();
    }

    public bool Save()
    {
        var saved= _dbContext.SaveChanges();
        return saved > 0 ? true : false;
    }

    public bool UpdateCategory(Category category)
    {
        _dbContext.Update(category);
        return Save();
    }

    public bool DeleteCategory(Category category)
    {
        _dbContext.Remove(category);
        return Save();
    }

    public Category GetCategory(int id)
    {
        var category = _dbContext.Categories.Where(c => c.Id == id).FirstOrDefault();
        return category;
    }
}