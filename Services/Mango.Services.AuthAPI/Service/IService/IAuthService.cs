using Mango.Services.AuthAPI.Models.Dto;

namespace Mango.Services.AuthAPI.Service.IService;

public interface IAuthService
{
    Task<LoginResponseDto> RegisterAsync(RegistrationRequestDto registrationRequestDto);
    Task<UserDto> LoginAsync(LoginRequestDto loginRequestDto);
}