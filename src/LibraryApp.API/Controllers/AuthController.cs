using LibraryApp.Application.Interfaces;
using LibraryApp.Shared.Requests;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("verify")]
    public IActionResult Verify([FromBody] LoginRequest request)
    {
        if (_authService.VerifyAdminPassword(request.Password))
        {
            return Ok();
        }
        return Unauthorized();
    }
}
