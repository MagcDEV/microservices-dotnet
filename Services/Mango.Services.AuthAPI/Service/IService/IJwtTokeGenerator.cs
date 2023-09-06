using Mango.Services.AuthAPI.Modules;

namespace Mango.Services.AuthAPI.Service.IService;

public interface IJwtTokeGenerator
{
    string GenerateToken(AppUser appUser);
}