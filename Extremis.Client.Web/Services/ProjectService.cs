using System.Net.Http.Json;
using Extremis.Client.Extensions;
using Extremis.Projects;

namespace Extremis.Client.Services;

public class ProjectService
{
    private readonly HttpClient _httpClient;

    public ProjectService(HttpClient httpClient) => _httpClient = httpClient;

    public async Task<PaginatedResult<ProjectDto>> GetAllMyProjects(int pageNumber, int pageSize)
    {
        return await _httpClient.GetFromJsonAsync<PaginatedResult<ProjectDto>>($"api/projects/own?pageNumber={pageNumber}&pageSize={pageSize}");
    }

    public async Task<PaginatedResult<ProjectDto>> GetAllJoinedProjects(int pageNumber, int pageSize)
    {
        return await _httpClient.GetFromJsonAsync<PaginatedResult<ProjectDto>>($"api/projects/joined?pageNumber={pageNumber}&pageSize={pageSize}");
    }

    public async Task<Result<ProjectFullDto>> GetProjectFullInfo(Guid id)
    {
        return await _httpClient.GetFromJsonAsync<Result<ProjectFullDto>>($"api/projects/{id}");
    }

    public async Task<Result<List<(string, string)>>> GetAllProjectsList()
    {
        return await _httpClient.GetFromJsonAsync<Result<List<(string, string)>>>($"api/projects/list");
    }

    public async Task<IResult> CreateProject(CreateProjectRequestDto request)
    {
        var response = await _httpClient.PostAsJsonAsync("api/projects", request);
        return await response.ToResult();
    }
}