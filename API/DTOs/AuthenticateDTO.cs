using System;

namespace API.DTOs;

public class AuthenticateDTO
{
    public required string UserName { get; set; }
    public required string Password { get; set; }
}
