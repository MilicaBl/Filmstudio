using System;

namespace API.Interfaces;

public interface IUserAuthenticate
{
    public string UserName { get; set; }
    public string Password { get; set; }
}
