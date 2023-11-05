using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiProject.Dto;
using WebApiProject.Interfaces;
using WebApiProject.Models;

namespace WebApiProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : Controller
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserController(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    [HttpGet]
    [ProducesResponseType(200,Type = typeof(IEnumerable<User>))]
    public IActionResult GetUsers()
    {
        var categories = _mapper.Map<List<UserDto>>(_userRepository.GetUsers());
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        return Ok(categories);
    }
    
    [HttpGet("{Id}/comments")]
    [ProducesResponseType(200, Type=typeof(IEnumerable<Article>))]
    public IActionResult GetArticleByCategory(int Id)
    {
        
        var coments = _mapper.Map<List<CommentDto>>(_userRepository.GetCommentsByUser(Id));

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(coments);
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(200,Type = typeof(IEnumerable<User>))]
    public IActionResult GetUser(int id)
    {
        var categories = _mapper.Map<UserDto>(_userRepository.GetUser(id));
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        return Ok(categories);
    }
}