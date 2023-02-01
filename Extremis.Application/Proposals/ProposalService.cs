using System.Linq.Expressions;
using Extremis.Extensions;
using Extremis.Repositories;
using Extremis.Wrapper;
using Microsoft.EntityFrameworkCore;

namespace Extremis.Proposals;

public class ProposalService : IProposalService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProposalService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PaginatedResult<ProposalDto>> GetAllMyProposals(int pageNumber, int pageSize, string userId)
    {
        try
        {
            Expression<Func<Proposal, ProposalDto>> expression = proposal => new ProposalDto()
            {
                CreatedOn = proposal.CreatedOn,
                Description = proposal.Description,
                Duration = proposal.Duration,
                ExpireTime = proposal.ExpireTime,
                Id = proposal.Id,
                LastModifiedOn = proposal.LastModifiedOn,
                ProposerId = proposal.ProposerId,
                ProposerName = proposal.Proposer.FullName,
                Status = proposal.Status,
                Title = proposal.Title
            };
            return await _unitOfWork.GetRepository<Proposal>().Entities
                .Include(x => x.Proposer)
                .Where(x => x.ProposerId == userId)
                .OrderByDescending(x => x.CreatedOn)
                .ThenByDescending(x => x.LastModifiedOn)
                .Select(expression)
                .ToPaginatedListAsync(pageNumber, pageSize);
        }
        catch (Exception e)
        {
            return PaginatedResult<ProposalDto>.Failure(new List<string>() { e.Message });
        }
    }

    public async Task<PaginatedResult<ProposalDto>> GetAllProposals(int pageNumber, int pageSize, string searchString, string orderBy, string userId)
    {
        try
        {
            Expression<Func<Proposal, ProposalDto>> expression = proposal => new ProposalDto()
            {
                CreatedOn = proposal.CreatedOn,
                Description = proposal.Description,
                Duration = proposal.Duration,
                ExpireTime = proposal.ExpireTime,
                Id = proposal.Id,
                LastModifiedOn = proposal.LastModifiedOn,
                ProposerId = proposal.ProposerId,
                ProposerName = proposal.Proposer.FullName,
                Status = proposal.Status,
                Title = proposal.Title
            };
            return await _unitOfWork.GetRepository<Proposal>().Entities
                .Include(x => x.Proposer)
                .Where(x => x.ProposerId != userId)
                .OrderByDescending(x => x.CreatedOn)
                .ThenByDescending(x => x.LastModifiedOn)
                .Select(expression)
                .ToPaginatedListAsync(pageNumber, pageSize);
        }
        catch (Exception e)
        {
            return PaginatedResult<ProposalDto>.Failure(new List<string>() { e.Message });
        }
    }

    public async Task<IResult<ProposalDto>> GetProposal(string id)
    {
        try
        {
            var proposal = await _unitOfWork.GetRepository<Proposal>().Entities
                .Include(x => x.Proposer)
                .FirstOrDefaultAsync(x => x.Id == id);
            var proposalDto = new ProposalDto()
            {
                CreatedOn = proposal.CreatedOn,
                Description = proposal.Description,
                ProposerId = proposal.ProposerId,
                ProposerName = proposal.Proposer.FullName,
                Status = proposal.Status,
                Title = proposal.Title,
                ExpireTime = proposal.ExpireTime,
                Duration = proposal.Duration,
                LastModifiedOn = proposal.LastModifiedOn,
                Id = proposal.Id
            };
            return await Result<ProposalDto>.SuccessAsync(proposalDto);
        }
        catch (Exception e)
        {
            return await Result<ProposalDto>.FailAsync(e.Message);
        }
    }

    public async Task<IResult> CreateProposal(CreateProposalRequestDto request, string userName, string userId)
    {
        try
        {
            var proposal = new Proposal()
            {
                CreatedBy = userName,
                CreatedOn = DateTime.Now,
                CreatedByUserId = userId,
                LastModifiedBy = userName,
                LastModifiedOn = DateTime.Now,
                LastModifiedByUserId = userId,
                Description = request.Description,
                Duration = request.Duration,
                ExpireTime = request.ExpireTime,
                Title = request.Title,
                ProposerId = userId,
                Status = ProposalStatus.Open
            };
            await _unitOfWork.GetRepository<Proposal>().AddAsync(proposal);
            await _unitOfWork.Commit();
            return await Result.SuccessAsync("Created proposal successfully!");
        }
        catch (Exception e)
        {
            return await Result.FailAsync(e.Message);
        }
    }
}