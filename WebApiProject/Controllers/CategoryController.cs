using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
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

    [HttpPost]
    [ProducesResponseType(204)]

    public IActionResult CreateCategory([FromBody] CategoryDto? categoryDto)
    {
        if (categoryDto == null)
        {
            return BadRequest(ModelState);
        }

        var category = _categoryRepository.GetCategories().FirstOrDefault(c => c.CategoryType.Trim().ToUpper()== categoryDto.CategoryType.Trim().ToUpper());
        if (category != null)
        {
            ModelState.AddModelError("","category already exists");
            return StatusCode(422, ModelState);
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var categoryMap = _mapper.Map<Category>(categoryDto);
        
        if (!_categoryRepository.CreateCategory(categoryMap))
        {
            ModelState.AddModelError("", "couldn't add data into database");
            return StatusCode(500, ModelState);
        }
        return Ok("Successfully created");
    }
}