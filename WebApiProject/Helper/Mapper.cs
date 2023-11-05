using AutoMapper;
using WebApiProject.Dto;
using WebApiProject.Models;

namespace WebApiProject.Helper;

public class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<Article, ArticleDto>();
        CreateMap<Category, CategoryDto>();
        CreateMap<Author, AuthorDto>();
        CreateMap<Comment, CommentDto>();
        CreateMap<User, UserDto>();
    }
}