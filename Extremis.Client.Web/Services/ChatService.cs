using System.Net.Http.Json;
using Extremis.Client.Extensions;
using Extremis.ProjectChats;
using Extremis.Projects;

namespace Extremis.Client.Services;

public class ChatService
{
    private readonly HttpClient _httpClient;

    public ChatService(HttpClient httpClient) => _httpClient = httpClient;
    
    public async Task<IResult<List<ProjectMemberDto>>> GetProjectMembers(string projectId)
    {
        return await _httpClient.GetFromJsonAsync<Result<List<ProjectMemberDto>>>(
            $"api/project-chat/members?projectId={projectId}");
    }

    public async Task<IResult> SaveMessageAsync(SendMessageDto request)
    {
        var response = await _httpClient.PostAsJsonAsync("api/project-chat/save", request);
        return await response.ToResult();
    }

    public async Task<IResult<List<MessageDto>>> GetConversation(string contactId, string projectId)
    {
        return await _httpClient.GetFromJsonAsync<Result<List<MessageDto>>>($"api/project-chat/get?contactId={contactId}&projectId={projectId}");
    }
}