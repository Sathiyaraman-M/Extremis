using Extremis.Wrapper;

namespace Extremis.Proposals;

public interface IProposalService
{
     Task<IResult<ProposalDto>> GetProposal(string id);
     Task<IResult> CreateProposal(CreateProposalRequestDto request);
}