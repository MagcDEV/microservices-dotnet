using Mango.Services.AuthAPI.Data;
using Mango.Services.AuthAPI.Models.Dto;
using Mango.Services.AuthAPI.Modules;
using Mango.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Identity;

namespace Mango.Services.AuthAPI.Service;

public class AuthService : IAuthService
{

    private readonly AppDbContext _db;
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AuthService(AppDbContext appContext,
                       UserManager<AppUser> userManager,
                       RoleManager<IdentityRole> roleManager)
    {
        _db = appContext;
        _userManager = userManager;
        _roleManager = roleManager;
    }
    


    public Task<UserDto> LoginAsync(LoginRequestDto loginRequestDto)
    {
        throw new NotImplementedException();
    }

    public Task<LoginResponseDto> RegisterAsync(RegistrationRequestDto registrationRequestDto)
    {
        throw new NotImplementedException();
    }
}