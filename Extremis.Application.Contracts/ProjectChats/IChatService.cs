using Extremis.Wrapper;

namespace Extremis.ProjectChats;

public interface IChatService
{
    Task<IResult> SaveChatMessageAsync(string fromId, SendMessageDto request);

    Task<IResult<List<MessageDto>>> GetConversationAsync(string contactId, string userId, string projectId);
}