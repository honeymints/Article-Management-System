using WebApiProject.Data;
using WebApiProject.Models;

namespace WebApiProject;

public class Seed
{
    private readonly ApplicationDbContext _dbContext;
    
    public Seed(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void SeedData()
    {

        if (!_dbContext.AuthorArticles.Any())
        {
            var authorsOfArticles = new List<AuthorArticles>()
            {
                new AuthorArticles()
                {
                    Article = new Article()
                    {
                        Title = "I love Roseanne Park",
                        Text = "I LOVE ROSEANNE SO MUCH",
                        Comments = new List<Comment>()
                        {
                            new Comment() { Text = "Hey, i like this article", User = new User(){ Name = "Kimberley", Surname="Ash"}},
                            new Comment() { Text = "ikr, she is so pretty!", User = new User(){ Name = "Dan", Surname="Conner"}}
                        },
                        CategoriesList = new List<CategoryArticles>()
                        {
                            new CategoryArticles() { Category = new Category(){ CategoryType = "Confession"} }
                        },
                    },
                    Author =  new Author(){ Name = "Aruzhan", Surname = "Ismagulova"}
                },
                new AuthorArticles()
                {
                    Article = new Article()
                    {
                        Title = "Park Chaeyoung is loml",
                        Text = "nyeheehhhehhehe",
                        Comments = new List<Comment>()
                        {
                            new Comment() { Text = "Good job!", User = new User(){Name = "Arman", Surname = "Murat"}},
                            new Comment() { Text = "I REALLY ENJOYED YOUR FINDINGS",User = new User(){Name = "Danny", Surname = "Phantom"} }
                        },
                        CategoriesList = new List<CategoryArticles>()
                        {
                            new CategoryArticles() { Category = new Category(){ CategoryType = "Statement"} }
                        },
                    },
                    Author =  new Author(){ Name = "Aruzhan", Surname = "Ismagulova"}
                }
                
            };
            _dbContext.AuthorArticles.AddRange(authorsOfArticles);
            _dbContext.SaveChanges();
        }
    }
}