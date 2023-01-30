using Extremis.Users;

namespace Extremis.Controllers;

[Route("api/users")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService) => _userService = userService;
    
    [HttpPost("update")]
    public async Task<IActionResult> UpdateUserInfo(UpdateAppUserRequestDto request)
    {
        var userId = HttpContext.User.FindFirstValue(JwtClaimTypes.Subject);
        return Ok(await _userService.UpdateDeveloperInfoAsync(request, userId));
    }
    
    [HttpGet("status")]
    public async Task<IActionResult> GetUserStatus()
    {
        var userId = HttpContext.User.FindFirstValue(JwtClaimTypes.Subject);
        return Ok(await _userService.GetUserStatusAsync(userId));
    }
    
    [HttpPost("status")]
    public async Task<IActionResult> UpdateUserStatus(UpdateUserStatusRequestDto request)
    {
        var userId = HttpContext.User.FindFirstValue(JwtClaimTypes.Subject);
        return Ok(await _userService.UpdateUserStatusAsync(request, userId));
    }
}