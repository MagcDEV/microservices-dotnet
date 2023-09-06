using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.AuthAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthApiController : ControllerBase
{

    [HttpPost("register")]
    public IActionResult Register()
    {
        return Ok();
    }
    
    [HttpPost("login")]
    public IActionResult Login()
    {
        return Ok();
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