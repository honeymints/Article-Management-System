﻿namespace WebApiProject.Dto;

public class UserDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public ICollection<CommentDto> Comments { get; set; }
}