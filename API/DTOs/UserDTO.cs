using System;
using API.Interfaces;

namespace API.DTOs;

public class UserDTO
{
    public required string Id { get; set; }
    public string? UserName { get; set; }
    public required string Role { get; set; } = "admin";
}
