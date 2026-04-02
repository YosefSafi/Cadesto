using Cadesto.Data.Entities;
using Cadesto.Logic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cadesto.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class AdminController : ControllerBase
{
    private readonly IPropertyService _propertyService;
    private readonly IInvitationManager _invitationManager;

    public AdminController(IPropertyService propertyService, IInvitationManager invitationManager)
    {
        _propertyService = propertyService;
        _invitationManager = invitationManager;
    }

    [HttpPost("houses")]
    public async Task<IActionResult> CreateHouse([FromBody] House house)
    {
        await _propertyService.CreateHouseAsync(house);
        return Ok(house);
    }

    [HttpPost("invite")]
    public async Task<IActionResult> InviteTenant([FromBody] string email)
    {
        var token = await _invitationManager.CreateInvitationAsync(email);
        return Ok(new { Email = email, Token = token });
    }

    // Additional CRUD endpoints for units, listings, etc. would go here
    [HttpGet("debug/users")]
    public IActionResult DebugUsers()
    {
        // This is just for demonstration
        return Ok("Admin debug endpoint");
    }
}
