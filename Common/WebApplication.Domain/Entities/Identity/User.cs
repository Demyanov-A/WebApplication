using Microsoft.AspNetCore.Identity;

namespace WebApplication.Domain.Entities.Identity;

public class User : IdentityUser
{
    public const string Administrator = "Admin";

    public const string DefaultAdminPassword = "StrongPass_0000";
}