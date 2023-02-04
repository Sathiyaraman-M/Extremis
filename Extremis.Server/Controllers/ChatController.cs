using Extremis.ProjectChats;

namespace Extremis.Controllers;

[Authorize]
[Route("api/project-chat")]
[ApiController]
public class ChatController : ControllerBase
{
    private readonly IChatService _chatService;

    public ChatController(IChatService chatService)
    {
        _chatService = chatService;
    }

    [HttpPost("save")]
    public async Task<IActionResult> SaveMessage(SendMessageDto request)
    {
        var userId = User.FindFirstValue(JwtClaimTypes.Subject);
        return Ok(await _chatService.SaveChatMessageAsync(userId, request));
    }
}