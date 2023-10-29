namespace WebApiProject.Models;

public class Category
{
    public int Id { get; set; }
    public string CategoryType { get; set; }
    public ICollection<CategoryArticles> ArticlesCollection { get; set; }
}