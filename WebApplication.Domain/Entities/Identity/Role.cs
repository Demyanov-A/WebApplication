using Microsoft.AspNetCore.Identity;

namespace WebApplication.Domain.Entities.Identity;

public class Role : IdentityRole
{
    public const string Administrators = "Administrators";

    public const string Users = "Users";
}