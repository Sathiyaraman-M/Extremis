using System.Net.Http.Json;
using Extremis.Client.Extensions;
using Extremis.Users;

namespace Extremis.Client.Services;

public class UserService
{
    private readonly HttpClient _httpClient;

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IResult<UserStatusDto>> GetUserStatus()
    {
        return await _httpClient.GetFromJsonAsync<Result<UserStatusDto>>("api/users/status");
    }

    public async Task<IResult> UpdateUserStatus(UpdateUserStatusRequestDto request)
    {
        var response = await _httpClient.PostAsJsonAsync("api/users/status", request);
        return await response.ToResult();
    }
}