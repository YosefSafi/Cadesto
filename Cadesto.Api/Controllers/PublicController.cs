using Cadesto.Logic.Services;
using Cadesto.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Cadesto.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PublicController : ControllerBase
{
    private readonly IAuthManager _authManager;
    private readonly IPropertyService _propertyService;

    public PublicController(IAuthManager authManager, IPropertyService propertyService)
    {
        _authManager = authManager;
        _propertyService = propertyService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var response = await _authManager.LoginAsync(request);
        if (response == null) return Unauthorized();
        return Ok(response);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegistrationRequest request)
    {
        var success = await _authManager.RegisterAsync(request);
        if (!success) return BadRequest("Registration failed");
        return Ok();
    }

    [HttpGet("listings")]
    public async Task<IActionResult> GetListings()
    {
        return Ok(await _propertyService.GetAllListingsAsync());
    }

    [HttpGet("properties")]
    public async Task<IActionResult> GetProperties()
    {
        return Ok(await _propertyService.GetAllPropertiesAsync());
    }
}
