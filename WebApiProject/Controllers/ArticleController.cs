using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApiProject.Dto;
using WebApiProject.Interfaces;
using WebApiProject.Models;
using WebApiProject.Repository;

namespace WebApiProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ArticleController : Controller
{
    private readonly IArticleRepository _article;
    private readonly IMapper _mapper;


    public ArticleController(IArticleRepository article, IMapper mapper)
    {
        _article = article;
        _mapper = mapper;
    }
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Article>))]
    public IActionResult GetArticles()
    {
        var articles = _mapper.Map<List<ArticleDto>>(_article.GetArticles());
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        return Ok(articles);
    }
    [HttpGet("{id}")]
    [ProducesResponseType(200, Type=typeof(Article))]
    [ProducesResponseType(400)]
    public IActionResult GetArticle(int id)
    {
        if (!_article.ArticleExists(id))
        {
            return NotFound();
        }

        var article = _mapper.Map<ArticleDto>(_article.GetArticle(id));
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(article);
    }
    
    [HttpGet("{articleId}/comments")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Comment>))]
    public IActionResult GetCommentsByArticle(int articleId)
    {
        if (!_article.ArticleExists(articleId))
        {
            return NotFound();
        }
        var comments = _mapper.Map<List<CommentDto>>(_article.GetCommentsByArticle(articleId));
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        return Ok(comments);
    }
}