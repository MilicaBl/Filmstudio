using System;
using API.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace API.Models;

public class ApplicationUser : IdentityUser, IUser
{
    public string Role { get; set; } = "admin";
}
