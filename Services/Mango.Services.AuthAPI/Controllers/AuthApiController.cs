using Mango.Services.AuthAPI.Models.Dto;
using Mango.Services.AuthAPI.Service.IService;
using Mango.Services.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.AuthAPI.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthApiController : ControllerBase
{
    private readonly IAuthService _authService;
    protected ResponseDto _response;

    public AuthApiController(IAuthService authService)
    {
        _authService = authService;
        _response = new();
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegistrationRequestDto model)
    {
        var errorMesagge = await _authService.RegisterAsync(model);
        if (!string.IsNullOrEmpty(errorMesagge))
        {
            _response.IsSuccess = false;
            _response.Message = errorMesagge;
            return BadRequest(_response);
        }
        
        return Ok(_response);
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
    {
        var loginResponse = await _authService.LoginAsync(model);
        if (loginResponse == null)
        {
            _response.IsSuccess = false;
            _response.Message = "Invalid credentials";
            return BadRequest(_response);
        }

        _response.IsSuccess = true;
        _response.Result = loginResponse;

        return Ok(_response);

    }
    
    [HttpPost("refresh-token")]
    public IActionResult RefreshToken()
    {
        return Ok();
    }
    
    [HttpPost("revoke-token")]
    public IActionResult RevokeToken()
    {
        return Ok();
    }
    
    [HttpPost("revoke-all-tokens")]
    public IActionResult RevokeAllTokens()
    {
        return Ok();
    }
    
    [HttpPost("change-password")]
    public IActionResult ChangePassword()
    {
        return Ok();
    }
    
    [HttpPost("forgot-password")]
    public IActionResult ForgotPassword()
    {
        return Ok();
    }
    
    [HttpPost("reset-password")]
    public IActionResult ResetPassword()
    {
        return Ok();
    }

}