using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cadesto.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Tenant,Admin")]
public class TenantController : ControllerBase
{
    [HttpGet("profile")]
    public IActionResult GetProfile()
    {
        var email = User.Identity?.Name;
        return Ok(new { Email = email, Message = "This is your tenant profile." });
    }

    [HttpPut("preferences")]
    public IActionResult UpdatePreferences([FromBody] object preferences)
    {
        return Ok(new { Message = "Preferences updated." });
    }
}
