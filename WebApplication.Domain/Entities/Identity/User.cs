using Microsoft.AspNetCore.Identity;

namespace WebApplication.Domain.Entities.Identity;

public class User : IdentityUser
{
    public string AboutMyself { get; set; }
}