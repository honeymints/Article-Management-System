﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApiProject.Dto;
using WebApiProject.Interfaces;
using WebApiProject.Models;

namespace WebApiProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorController : Controller
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;

    public AuthorController(IAuthorRepository authorRepository, IMapper mapper)
    {
        _authorRepository = authorRepository;
        _mapper = mapper;
    }
    
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Author>))]
    public IActionResult GetAuthors()
    {
        var authors = _mapper.Map<List<AuthorDto>>(_authorRepository.GetAuthors());
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
        var author = _mapper.Map<AuthorDto>(_authorRepository.GetAuthor(id));
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(author);
    }

    [HttpGet("/{id}/articles")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Article>))]
    public IActionResult GetArticlesByAuthors(int id)
    {
        var articles = _mapper.Map<List<ArticleDto>>(_authorRepository.GetArticleByAuthor(id));
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(articles);
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public IActionResult CreateAuthor([FromBody] AuthorDto authorDto)
    {
        var author = _authorRepository.GetAuthor(authorDto.Id);
        if (author != null)
        {
            ModelState.AddModelError("", "such author already exists");
            return StatusCode(422, ModelState);
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var authorCreate = _mapper.Map<Author>(authorDto);

        if (!_authorRepository.CreateAuthor(authorCreate))
        {
            ModelState.AddModelError("","couldn't add author to db");
            return StatusCode(500, ModelState);
        }

        return Ok("Successfully created!");

    }
    
}