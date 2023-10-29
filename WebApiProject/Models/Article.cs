namespace WebApiProject.Models;

public class Article
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Text { get; set; } 
    public DateTime PublishedDate { get; set; }
    public ICollection<Comment> Comments { get; set; }
    public ICollection<AuthorArticles> AuthorsList { get; set; }
    public ICollection<CategoryArticles> CategoriesList { get; set; }
}