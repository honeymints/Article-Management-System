using WebApiProject.Models;

namespace WebApiProject.Interfaces;

public interface ICategoryRepository
{
    public List<Category> GetCategories();

    /*public List<Category> GetCategoriesByName(string name);*/
    public bool CategoryExists(int id);

    public bool CategoryExists(string categoryType);
    public ICollection<Article> GetArticlesByCategory(int id);
    public Category GetCategory(int id);
    
    public bool CreateCategory(Category category);
    public bool Save();

    public bool UpdateCategory(Category category);
    public bool DeleteCategory(Category category);




}