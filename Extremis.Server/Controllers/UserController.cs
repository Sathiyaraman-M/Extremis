using Extremis.Users;

namespace Extremis.Controllers;

[Route("api/users")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService) => _userService = userService;

    [HttpPost("update")]
    public async Task<IActionResult> UpdateUserInfo(UpdateAppUserDto request)
    {
        var userId = HttpContext.User.FindFirstValue(JwtClaimTypes.Subject);
        return Ok(await _userService.UpdateDeveloperInfo(request, userId));
    }
}