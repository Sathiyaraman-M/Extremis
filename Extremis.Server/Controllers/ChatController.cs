using Extremis.ProjectChats;

namespace Extremis.Controllers;

[Authorize]
[Route("api/project-chat")]
[ApiController]
public class ChatController : ControllerBase
{
    private readonly IChatService _chatService;

    public ChatController(IChatService chatService) => _chatService = chatService;

    [HttpGet("members")]
    public async Task<IActionResult> GetProjectMembers(string projectId)
    {
        return Ok(await _chatService.GetProjectMembers(projectId));
    }

    [HttpPost("save")]
    public async Task<IActionResult> SaveMessage(SendMessageDto request)
    {
        var userId = User.FindFirstValue(JwtClaimTypes.Subject);
        return Ok(await _chatService.SaveChatMessageAsync(userId, request));
    }

    [HttpGet("get")]
    public async Task<IActionResult> GetConversation(string contactId, string projectId)
    {
        var userId = User.FindFirstValue(JwtClaimTypes.Subject);
        return Ok(await _chatService.GetConversationAsync(contactId, userId, projectId));
    }
}