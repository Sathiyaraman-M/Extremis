using Extremis.Users;

namespace Extremis.Controllers;

[Route("api/users")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService) => _userService = userService;
    
    [HttpGet("info")]
    public async Task<IActionResult> GetUserInfo()
    {
        var userId = HttpContext.User.FindFirstValue(JwtClaimTypes.Subject);
        return Ok(await _userService.GetUserInfoAsync(userId));
    }
    
    [HttpPost("info")]
    public async Task<IActionResult> UpdateUserInfo(UpdateUserInfoRequestDto request)
    {
        var userId = HttpContext.User.FindFirstValue(JwtClaimTypes.Subject);
        return Ok(await _userService.UpdateUserInfoAsync(request, userId));
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