using System;

namespace API.DTOs;

public class UserRegisterDTO
{
    public required string UserName { get; set; }
    public required string Password { get; set; }
    public bool IsAdmin { get; set; }
}
