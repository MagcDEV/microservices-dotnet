using Microsoft.AspNetCore.Identity;

namespace Mango.Services.AuthAPI.Modules;

public class AppUser : IdentityUser
{
    public required string Name { get; set; }
    
}