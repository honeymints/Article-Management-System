namespace WebApiProject.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public ICollection<Comment> Comments { get; set; }
    
}