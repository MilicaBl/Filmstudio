using System;

namespace API.Interfaces;

public interface IUserRegister
{
    string UserName { get; set; }
    string Password { get; set; }
    bool IsAdmin { get; set; }
}
