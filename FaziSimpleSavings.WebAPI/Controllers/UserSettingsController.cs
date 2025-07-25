using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserSettingsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetSettings()
    {
        // Example secured action
        return Ok(new { Message = "This endpoint is secured with JWT" });
    }
}
