using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApiProject.Dto;
using WebApiProject.Interfaces;
using WebApiProject.Models;

namespace WebApiProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentController : Controller
{
    private readonly IArticleRepository _articleRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICommentRepository _commentRepository;
    private readonly IMapper _mapper;

    public CommentController(IArticleRepository articleRepository, IUserRepository userRepository, ICommentRepository commentRepository, IMapper mapper)
    {
        _articleRepository = articleRepository;
        _userRepository = userRepository;
        _commentRepository = commentRepository;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Author>))]
    public IActionResult GetAuthors()
    {
        var authors = _mapper.Map<List<CommentDto>>(_commentRepository.GetComments());
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        return Ok(authors);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(200, Type = typeof(Author))]
    public IActionResult GetAuthor(int id)
    {
        var author = _mapper.Map<CommentDto>(_commentRepository.GetComment(id));
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(author);
    }

    [HttpPost("/{articleId}/comment")]
    [ProducesResponseType(201)]
    public IActionResult CreateComment([FromQuery] int userId, int articleId, [FromBody] CommentDto? commentDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var user = _userRepository.GetUser(userId);
        var article = _articleRepository.GetArticle(articleId);
        
        if (user == null || article==null)
        {
            ModelState.AddModelError("","there is no such user or article");
            return StatusCode(422, ModelState);
        }

        
        var commentCreate = _mapper.Map<Comment>(commentDto);

        if (!_commentRepository.CreateComment(article, user, commentCreate))
        {
            ModelState.AddModelError("","couldn't add comment to db");
            return StatusCode(500, ModelState);
        }

        return Ok("Successfully created");
    }
}