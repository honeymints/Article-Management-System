using Microsoft.EntityFrameworkCore;
using WebApiProject.Models;

namespace WebApiProject.Data;

public class ApplicationDbContext : DbContext
{
    private readonly DbContextOptions<ApplicationDbContext> _context;
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Article> Articles { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<CategoryArticles> CategoryArticles { get; set; }
    public DbSet<AuthorArticles> AuthorArticles  { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuthorArticles>().HasKey(p => new { p.AuthorId, p.ArticleID });
        modelBuilder.Entity<AuthorArticles>().HasOne(p => p.Article)
            .WithMany(p => p.AuthorsList).HasForeignKey(p=>p.ArticleID);

        modelBuilder.Entity<AuthorArticles>().HasOne(p => p.Author).WithMany(p => p.ArticlesCollection)
            .HasForeignKey(p => p.AuthorId);
        
        modelBuilder.Entity<CategoryArticles>().HasKey(p => new { p.ArticleId, p.CategoryId });
        
        modelBuilder.Entity<CategoryArticles>().HasOne(p => p.Category).WithMany(p => p.ArticlesCollection)
            .HasForeignKey(p => p.CategoryId);
        modelBuilder.Entity<CategoryArticles>().HasOne(p => p.Article).WithMany(p => p.CategoriesList)
            .HasForeignKey(p => p.ArticleId);
    }
}