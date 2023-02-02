using Extremis.Wrapper;

namespace Extremis.Proposals;

public interface IProposalService
{
     Task<PaginatedResult<ProposalDto>> GetAllMyProposals(int pageNumber, int pageSize, string searchString, string userId);
     Task<PaginatedResult<ProposalDto>> GetAllProposals(int pageNumber, int pageSize, string searchString, string orderBy, string userId);
     Task<IResult<ProposalDto>> GetProposal(string id);
     Task<IResult> CreateProposal(CreateProposalRequestDto request, string userName, string userId);
     Task<IResult> ApplyForProposal(ApplyForProposalRequestDto request, string userName, string userId);
     Task<PaginatedResult<ReciprocatorDto>> GetAllCandidates(int pageNumber, int pageSize, string searchString, string id, string userId);
     Task<IResult> CloseProposal(string id, string userId);
     Task<IResult> CancelProposal(string id, string userId);
}