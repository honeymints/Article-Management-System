﻿using WebApiProject.Data;
using WebApiProject.Interfaces;
using WebApiProject.Models;

namespace WebApiProject.Repository;

public class ArticleRepository : IArticleRepository
{
    private readonly ApplicationDbContext _context;

    public ArticleRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Article GetArticle(string name)
    {
        var articleByName = _context.Articles.FirstOrDefault(p => p.Title == name);
        return articleByName;
    }
    
    public Article GetArticle(int id)
    {
        var aritlceById = _context.Articles.FirstOrDefault(p => p.Id == id);
        return aritlceById;
    }

    public bool ArticleExists(int id)
    {
        return _context.Articles.Any(p => p.Id == id);
    }

    public ICollection<Article> GetArticles()
    {
        return _context.Articles.OrderBy(a => a.Id).ToList();
    }

    
}