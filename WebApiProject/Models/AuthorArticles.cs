namespace WebApiProject.Models;

public class AuthorArticles
{
    public int AuthorId { get; set; }
    public int ArticleID { get; set; }
    public Author Author { get; set; }
    public Article Article { get; set; }
}