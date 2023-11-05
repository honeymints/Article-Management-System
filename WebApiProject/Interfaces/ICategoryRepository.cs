using WebApiProject.Models;

namespace WebApiProject.Interfaces;

public interface ICategoryRepository
{
    public List<Category> GetCategories();

    /*public List<Category> GetCategoriesByName(string name);*/
    public bool CategoryExists(int id);

    public ICollection<Article> GetArticlesByCategory(int id);
    
    public ICollection<Article> GetArticlesByCategory(string type);

}