﻿using WebApiProject.Data;
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

    public ICollection<Article> GetArticlesByCategory(int id)
    {
        var articles = _dbContext.CategoryArticles.Where(p => p.CategoryId == id).Select(p => p.Article).ToList();
        return articles;
    }

    public ICollection<Article> GetArticlesByCategory(string type)
    {
        throw new NotImplementedException();
    }
}