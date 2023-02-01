using System.Net.Http.Json;
using Extremis.Client.Extensions;
using Extremis.Proposals;

namespace Extremis.Client.Services;

public class ProposalService
{
    private readonly HttpClient _httpClient;

    public ProposalService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<PaginatedResult<ProposalDto>> GetAllMyProposals(int pageNumber, int pageSize, string searchString)
    {
        return await _httpClient.GetFromJsonAsync<PaginatedResult<ProposalDto>>($"api/proposals/mine?pageNumber={pageNumber}&pageSize={pageSize}&searchString={searchString}");
    }

    public async Task<PaginatedResult<ProposalDto>> GetAllProposals(int pageNumber, int pageSize, string searchString)
    {
        return await _httpClient.GetFromJsonAsync<PaginatedResult<ProposalDto>>($"api/proposals?pageNumber={pageNumber}&pageSize={pageSize}&searchString={searchString}");
    }

    public async Task<IResult<ProposalDto>> GetProposal(string id)
    {
        return await _httpClient.GetFromJsonAsync<Result<ProposalDto>>($"api/proposals/{id}");
    }

    public async Task<IResult<ProposalDto>> GetProposal(Guid id)
    {
        return await _httpClient.GetFromJsonAsync<Result<ProposalDto>>($"api/proposals/{id}");
    }
    
    public async Task<IResult> CreateProposal(CreateProposalRequestDto request)
    {
        var response = await _httpClient.PostAsJsonAsync("api/proposals/create", request);
        return await response.ToResult();
    }
    
    public async Task<IResult> ApplyForProposal(ApplyForProposalRequestDto request)
    {
        var response = await _httpClient.PostAsJsonAsync("api/proposals/apply", request);
        return await response.ToResult();
    }

    public async Task<PaginatedResult<ReciprocatorDto>> GetAllCandidates(int pageNumber, int pageSize, string searchString, string id)
    {
        return await _httpClient.GetFromJsonAsync<PaginatedResult<ReciprocatorDto>>($"api/proposals/candidates?pageNumber={pageNumber}&pageSize={pageSize}&searchString={searchString}&id={id}");
    }
}