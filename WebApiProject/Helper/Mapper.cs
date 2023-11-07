using AutoMapper;
using WebApiProject.Dto;
using WebApiProject.Models;

namespace WebApiProject.Helper;

public class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<Article, ArticleDto>().ReverseMap();
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<Author, AuthorDto>().ReverseMap();
        CreateMap<Comment, CommentDto>().ReverseMap();
        CreateMap<User, UserDto>().ReverseMap();
        
    }
}