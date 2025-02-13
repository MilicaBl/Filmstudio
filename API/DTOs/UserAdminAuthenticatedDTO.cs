using System;

namespace API.DTOs;

public class UserAdminAuthenticatedDTO
{
    public required string Id { get; set; }
    public required string UserName { get; set; }
    public required string Role { get; set; }
    public string Token { get; set; } = string.Empty;
}
