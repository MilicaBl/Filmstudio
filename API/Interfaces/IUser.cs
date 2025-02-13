using System;
using System.Data.SqlTypes;

namespace API.Interfaces;

public interface IUser
{
    string Id { get; set; }
    string? UserName { get; set; }
    string Role { get; set; }
}
