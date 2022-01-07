using Microsoft.AspNetCore.Identity;

namespace WebApplication.Domain.Entities.Identity;

public class Role : IdentityRole
{
    public string Description { get; set; }
}