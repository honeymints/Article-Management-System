using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiProject.Dto;
using WebApiProject.Interfaces;
using WebApiProject.Models;

namespace WebApiProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentController : Controller
{
    private readonly ICommentRepository _commentRepository;
    private readonly IMapper _mapper;

    public CommentController(ICommentRepository commentRepository, IMapper mapper)
    {
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
}