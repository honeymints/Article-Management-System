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
    private readonly IAuthorRepository _authorRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IArticleRepository _article;
    private readonly IMapper _mapper;


    public ArticleController(IAuthorRepository authorRepository, ICategoryRepository categoryRepository, IArticleRepository article, IMapper mapper)
    {
        _authorRepository = authorRepository;
        _categoryRepository = categoryRepository;
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

    [HttpPost]
    [ProducesResponseType(201)]
    public IActionResult CreateArticle([FromQuery] int authorId, [FromQuery] int categoryId, [FromBody] ArticleDto articleDto)
    {
        var author = _authorRepository.GetAuthor(authorId);
        var category = _categoryRepository.GetCategory(categoryId);
        if (category==null)
        {
            ModelState.AddModelError("","there is no such category");
            return StatusCode(422, ModelState);
        }
        if (author == null)
        {
            ModelState.AddModelError("","there is no such author");
            return StatusCode(422, ModelState);
        }

        if (_article.ArticleExists(articleDto.Id))
        {
            ModelState.AddModelError("","article already exists");
            return StatusCode(422, ModelState);
        }
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var articleCreate = _mapper.Map<Article>(articleDto);
        if (!_article.CreateArticle(author, category, articleCreate))
        {
            ModelState.AddModelError("","couldn't create article");
            return StatusCode(500, ModelState);
        }
        
        return Ok();
    }
}