using DesafioPerfildeRisco.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost("login")]
    [AllowAnonymous]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        if (request.Username == "admin" && request.Password == "123")
        {
            var token = TokenService.GenerateToken(request.Username, "admin");
            return Ok(new { token });
        }
        return Unauthorized();
    }
}
