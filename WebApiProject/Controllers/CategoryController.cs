using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiProject.Dto;
using WebApiProject.Interfaces;
using WebApiProject.Models;

namespace WebApiProject.Controllers;


[Route("api/[controller]")]
[ApiController]
public class CategoryController : Controller
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }
    
    [HttpGet]
    [ProducesResponseType(200, Type=typeof(IEnumerable<Category>))]
    public IActionResult GetCategories()
    {
        var categories = _mapper.Map<List<CategoryDto>>(_categoryRepository.GetCategories());
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        return Ok(categories);
    }
    
    [HttpGet("/article/{categoryId}")]
    [ProducesResponseType(200, Type=typeof(IEnumerable<Article>))]
    public IActionResult GetArticleByCategory(int categoryId)
    {
        if (!_categoryRepository.CategoryExists(categoryId))
        {
            return NotFound();
        }
        var article = _mapper.Map<List<ArticleDto>>(_categoryRepository.GetArticlesByCategory(categoryId));

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(article);
    }
}