using Extremis.Repositories;
using Extremis.Wrapper;

namespace Extremis.Proposals;

public class ProposalService : IProposalService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProposalService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<IResult<ProposalDto>> GetProposal(string id)
    {
        throw new NotImplementedException();
    }

    public Task<IResult> CreateProposal(CreateProposalRequestDto request)
    {
        throw new NotImplementedException();
    }
}