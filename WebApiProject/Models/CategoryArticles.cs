namespace WebApiProject.Models;

public class CategoryArticles
{
    public int CategoryId { get; set; }
    public int ArticleId { get; set; }
    public Category Category { get; set; }
    public Article Article { get; set; }
}