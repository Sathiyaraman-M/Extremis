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
        return await _httpClient.GetFromJsonAsync<PaginatedResult<ProposalDto>>("api/proposals/mine");
    }

    public async Task<PaginatedResult<ProposalDto>> GetAllProposals(int pageNumber, int pageSize, string searchString)
    {
        return await _httpClient.GetFromJsonAsync<PaginatedResult<ProposalDto>>("api/proposals");
    }
    
    public async Task<IResult> CreateProposal(CreateProposalRequestDto request)
    {
        var response = await _httpClient.PostAsJsonAsync("api/proposals/create", request);
        return await response.ToResult();
    }
}