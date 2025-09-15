using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Interfaces;
using TaskManager.Application.Services;
using TaskManager.Domain.Entities;
using System.Security.Cryptography;
using System.Text;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IUserRepository _users;
    private readonly AuthService _auth;

    public AccountController(IUserRepository users, AuthService auth)
    {
        _users = users;
        _auth = auth;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(User user)
    {
        if (await _users.GetByEmailAsync(user.Email) != null)
            return BadRequest("Email already exists");

        user.PasswordHash = HashPassword(user.PasswordHash);

        await _users.AddAsync(user);
        await _users.SaveChangesAsync();
        return Ok("User registered successfully");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(string email, string password)
    {
        var user = await _users.GetByEmailAsync(email);
        if (user == null) return Unauthorized("Invalid credentials");

        if (user.PasswordHash != HashPassword(password))
            return Unauthorized("Invalid credentials");

        var token = _auth.GenerateToken(user);
        return Ok(new { token });
    }

    private string HashPassword(string password)
    {
        using var sha = SHA256.Create();
        var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }
}
