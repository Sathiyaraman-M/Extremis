using System.Net.Http.Json;
using Extremis.Client.Extensions;
using Extremis.Proposals;

namespace Extremis.Client.Services;

public class ProposalService
{
    private readonly HttpClient _httpClient;

    public ProposalService(HttpClient httpClient) => _httpClient = httpClient;

    public async Task<IResult<bool>> IsProposalPresent(string projectId)
    {
        return await _httpClient.GetFromJsonAsync<Result<bool>>($"api/proposals/check-open?projectId={projectId}");
    }
    
    public async Task<IResult> InitiateProposal(InitiateProposalRequestDto request)
    {
        var response = await _httpClient.PostAsJsonAsync($"api/proposals/{request.ProjectId}", request);
        return await response.ToResult();
    }
    
    public async Task<IResult> CloseProposal(string projectId)
    {
        return await _httpClient.GetFromJsonAsync<Result>($"api/proposals/close?projectId={projectId}");
    }
    public async Task<IResult> ApplyForProposal(ApplyForProposalRequestDto request)
    {
        var response = await _httpClient.PostAsJsonAsync("api/proposals/apply", request);
        return await response.ToResult();
    }
    
    public async Task<IResult<bool>> CheckAppliedStatus(string id)
    {
        return await _httpClient.GetFromJsonAsync<Result<bool>>($"api/proposals/apply-check?id={id}");
    }
    
    public async Task<PaginatedResult<ReciprocatorDto>> GetAllCandidates(int pageNumber, int pageSize, string searchString, string id)
    {
        return await _httpClient.GetFromJsonAsync<PaginatedResult<ReciprocatorDto>>($"api/proposals/candidates?pageNumber={pageNumber}&pageSize={pageSize}&searchString={searchString}&id={id}");
    }

    // public async Task<PaginatedResult<ProposalDto>> GetAllMyProposals(int pageNumber, int pageSize, string searchString)
    // {
    //     return await _httpClient.GetFromJsonAsync<PaginatedResult<ProposalDto>>($"api/proposals/mine?pageNumber={pageNumber}&pageSize={pageSize}&searchString={searchString}");
    // }
    //
    // public async Task<PaginatedResult<ProposalDto>> GetAllProposals(int pageNumber, int pageSize, string searchString)
    // {
    //     return await _httpClient.GetFromJsonAsync<PaginatedResult<ProposalDto>>($"api/proposals?pageNumber={pageNumber}&pageSize={pageSize}&searchString={searchString}");
    // }
    //
    // public async Task<IResult<ProposalDto>> GetProposal(string id)
    // {
    //     return await _httpClient.GetFromJsonAsync<Result<ProposalDto>>($"api/proposals/{id}");
    // }
    //
    // public async Task<IResult<ProposalDto>> GetProposal(Guid id)
    // {
    //     return await _httpClient.GetFromJsonAsync<Result<ProposalDto>>($"api/proposals/{id}");
    // }
    //
    // public async Task<IResult> CreateProposal(InitiateProposalRequestDto request)
    // {
    //     var response = await _httpClient.PostAsJsonAsync("api/proposals/create", request);
    //     return await response.ToResult();
    // }
    
    //
    // public async Task<IResult> CloseProposal(Guid id)
    // {
    //     return await _httpClient.GetFromJsonAsync<Result>($"api/proposals/close?id={id}");
    // }
    //
    // public async Task<IResult> CloseProposal(string id)
    // {
    //     return await _httpClient.GetFromJsonAsync<Result>($"api/proposals/close?id={id}");
    // }
    //
    // public async Task<IResult> CancelProposal(Guid id)
    // {
    //     return await _httpClient.GetFromJsonAsync<Result>($"api/proposals/cancel?id={id}");
    // }
    //
    // public async Task<IResult> CancelProposal(string id)
    // {
    //     return await _httpClient.GetFromJsonAsync<Result>($"api/proposals/cancel?id={id}");
    // }
}