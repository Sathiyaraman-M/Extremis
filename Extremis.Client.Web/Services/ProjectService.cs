using System.Net.Http.Json;
using Extremis.Client.Extensions;
using Extremis.Projects;

namespace Extremis.Client.Services;

public class ProjectService
{
    private readonly HttpClient _httpClient;

    public ProjectService(HttpClient httpClient) => _httpClient = httpClient;

    public async Task<PaginatedResult<ProjectDto>> GetAllMyProposals(int pageNumber, int pageSize)
    {
        return await _httpClient.GetFromJsonAsync<PaginatedResult<ProjectDto>>($"api/projects/own?pageNumber={pageNumber}&pageSize={pageSize}");
    }

    public async Task<PaginatedResult<ProjectDto>> GetAllJoinedProposals(int pageNumber, int pageSize)
    {
        return await _httpClient.GetFromJsonAsync<PaginatedResult<ProjectDto>>($"api/projects/joined?pageNumber={pageNumber}&pageSize={pageSize}");
    }

    public async Task<IResult> CreateProject(CreateProjectRequestDto request)
    {
        var response = await _httpClient.PostAsJsonAsync("api/projects", request);
        return await response.ToResult();
    }
}