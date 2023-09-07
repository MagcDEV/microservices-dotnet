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
    private readonly IJwtTokeGenerator _jwtTokenGenerator;

    public AuthService(AppDbContext appContext,
                       UserManager<AppUser> userManager,
                       RoleManager<IdentityRole> roleManager,
                       IJwtTokeGenerator jwtTokenGenerator)
    {
        _db = appContext;
        _userManager = userManager;
        _roleManager = roleManager;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<bool> AssingRole(string email, string roleName)
    {
        var user = _db.AppUsers.Where(u => u.Email.ToLower() == email.ToLower()).FirstOrDefault();
        if (user != null)
        {
            if (!_roleManager.RoleExistsAsync(roleName).Result)
            {
                // create role
                _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
            }

            await _userManager.AddToRoleAsync(user, roleName);
            return true;
        }

        return false;

    }

    public async Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto)
    {
        var user = _db.AppUsers.Where(u => u.UserName.ToLower() == loginRequestDto.UserName).FirstOrDefault();
        bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

        if (user == null || !isValid)
        {
            return new LoginResponseDto()
            {
                User = null,
                Token = "",
            };
        }
        
        UserDto userDto = new()
        {
            Email = user.Email,
            ID = user.Id,
            Name = user.Name,
            PhoneNumber = user.PhoneNumber,
        };


        return new LoginResponseDto()
        {
            User = userDto,
            Token = _jwtTokenGenerator.GenerateToken(user),
        };
        
    }

    public async Task<string> RegisterAsync(RegistrationRequestDto registrationRequestDto)
    {
        AppUser appUser = new()
        {
            UserName = registrationRequestDto.Email,
            Email = registrationRequestDto.Email,
            NormalizedEmail = registrationRequestDto.Email.ToUpper(),
            Name = registrationRequestDto.Name,
            PhoneNumber = registrationRequestDto.PhoneNumber,
        };

        try
        {
            var result = await _userManager.CreateAsync(appUser, registrationRequestDto.Password);
            if (result.Succeeded)
            {
                var userToReturn = _db.AppUsers.Where(u => u.Email == appUser.Email).FirstOrDefault();

                UserDto userDto = new()
                {
                    Email = userToReturn.Email,
                    ID = userToReturn.Id,
                    Name = userToReturn.Name,
                    PhoneNumber = userToReturn.PhoneNumber,
                };
                return "";
            }
            else
            {
                return result.Errors.FirstOrDefault().Description;
            
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return ex.Message;
            
        }

    }

}