using Extremis.Projects;
using Extremis.Repositories;
using Extremis.Users;
using Extremis.Wrapper;
using Microsoft.EntityFrameworkCore;

namespace Extremis.ProjectChats;

public class ChatService : IChatService
{
    private readonly IUnitOfWork _unitOfWork;

    public ChatService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<IResult<List<ProjectMemberDto>>> GetProjectMembers(string projectId)
    {
        try
        {
            var members = await _unitOfWork.GetRepository<ProjectMember>().Entities
                .Include(x => x.Member)
                .Where(x => x.ProjectId == projectId)
                .Select(x => x.Member)
                .Select(x => new ProjectMemberDto()
                {
                    CustomStatus = x.CustomStatus,
                    Id = x.Id,
                    FullName = x.FullName,
                    Status = x.Status,
                    UserName = x.UserName
                })
                .ToListAsync();
            return await Result<List<ProjectMemberDto>>.SuccessAsync(members);
        }
        catch (Exception e)
        {
            return await Result<List<ProjectMemberDto>>.FailAsync(e.Message);
        }
    }

    public async Task<IResult> SaveChatMessageAsync(string fromId, SendMessageDto request)
    {
        try
        {
            var chatMessage = new ChatMessage()
            {
                FromUserId = fromId,
                ToUserId = request.ToUserId,
                ProjectId = request.ProjectId,
                Message = request.Message,
                CreateDateTime = DateTime.Now
            };
            await _unitOfWork.GetRepository<ChatMessage>().AddAsync(chatMessage);
            await _unitOfWork.Commit();
            return await Result.SuccessAsync();
        }
        catch (Exception e)
        {
            return await Result.FailAsync(e.Message);
        }
    }

    public async Task<IResult<List<MessageDto>>> GetConversationAsync(string contactId, string userId, string projectId)
    {
        try
        {
            var messages = await _unitOfWork.GetRepository<ChatMessage>().Entities
                .Include(x => x.FromUser)
                .Include(x => x.ToUser)
                .OrderBy(x => x.CreateDateTime)
                .Where(x => (x.FromUserId == contactId && x.ToUserId == userId) ||
                            (x.ToUserId == userId && x.FromUserId == contactId))
                .Select(x => new MessageDto()
                {
                    Id = x.Id,
                    Message = x.Message,
                    CreateDateTime = x.CreateDateTime,
                    ProjectId = x.ProjectId,
                    FromUserId = x.FromUserId,
                    FromUserName = x.FromUser.UserName,
                    ToUserId = x.ToUserId,
                    ToUserName = x.ToUser.UserName
                })
                .ToListAsync();
            return await Result<List<MessageDto>>.SuccessAsync(messages);
        }
        catch (Exception e)
        {
            return await Result<List<MessageDto>>.FailAsync(e.Message);
        }
    }
}