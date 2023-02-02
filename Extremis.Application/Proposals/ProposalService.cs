using System.Linq.Expressions;
using Extremis.Extensions;
using Extremis.Repositories;
using Extremis.Specifications;
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

    public async Task<PaginatedResult<ProposalDto>> GetAllMyProposals(int pageNumber, int pageSize, string searchString, string userId)
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
                .Specify(new ProposalSearchSpecification(searchString))
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
                Title = proposal.Title,
            };
            return await _unitOfWork.GetRepository<Proposal>().Entities
                .Include(x => x.Proposer)
                .Where(x => x.ProposerId != userId)
                .Specify(new ProposalSearchSpecification(searchString))
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
            if (string.IsNullOrEmpty(request.ProjectId))
            {
                throw new Exception("Project Not Selected!");
            }
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
                Status = ProposalStatus.Open,
                ProjectId = request.ProjectId
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

    public async Task<IResult> ApplyForProposal(ApplyForProposalRequestDto request, string userName, string userId)
    {
        try
        {
            var reciprocation = new Reciprocation()
            {
                CreatedBy = userName,
                CreatedByUserId = userId,
                CreatedOn = DateTime.Now,
                Note = request.Note,
                ProposalId = request.ProposalId,
                ReciprocatorId = userId,
                LastModifiedByUserId = userId,
                LastModifiedBy = userName,
                LastModifiedOn = DateTime.Now
            };
            await _unitOfWork.GetRepository<Reciprocation>().AddAsync(reciprocation);
            await _unitOfWork.Commit();
            return await Result.SuccessAsync();
        }
        catch (Exception e)
        {
            return await Result.FailAsync(e.Message);
        }
    }

    public async Task<IResult<bool>> CheckApplyStatusProposal(string id, string userId)
    {
        try
        {
            var application = await _unitOfWork.GetRepository<Reciprocation>()
                .Entities.FirstOrDefaultAsync(x => x.ProposalId == id && x.ReciprocatorId == userId);
            return await Result<bool>.SuccessAsync(application != null);
        }
        catch (Exception e)
        {
            return await Result<bool>.FailAsync(e.Message);
        }
    }

    public async Task<PaginatedResult<ReciprocatorDto>> GetAllCandidates(int pageNumber, int pageSize, string searchString, string id, string userId)
    {
        try
        {
            Expression<Func<Reciprocation, ReciprocatorDto>> expression = reciprocation => new ReciprocatorDto()
            {
                CreatedOn = reciprocation.CreatedOn,
                Id = reciprocation.Id,
                ReciprocatorId = reciprocation.ReciprocatorId,
                ReciprocatorName = reciprocation.Reciprocator.FullName,
                Note = reciprocation.Note,
                CreatedByUserId = reciprocation.CreatedByUserId,
                CreatedBy = reciprocation.CreatedBy,
                ProposalId = reciprocation.ProposalId
            };
            return await _unitOfWork.GetRepository<Reciprocation>().Entities
                .Include(x => x.Proposal)
                .Include(x => x.Reciprocator)
                .Where(x => x.ProposalId == id && x.Proposal.ProposerId == userId)
                .Specify(new ReciprocationSearchSpecification(searchString))
                .OrderByDescending(x => x.CreatedOn)
                .ThenByDescending(x => x.LastModifiedOn)
                .Select(expression)
                .ToPaginatedListAsync(pageNumber, pageSize);
        }
        catch (Exception e)
        {
            return PaginatedResult<ReciprocatorDto>.Failure(new List<string>() { e.Message });
        }
    }

    public async Task<IResult> CloseProposal(string id, string userId)
    {
        try
        {
            var proposal = await _unitOfWork.GetRepository<Proposal>().GetByIdAsync(id);
            if (proposal.ProposerId != userId)
                throw new Exception("Cannot close proposal which you didn't create");
            proposal.Status = ProposalStatus.Closed;
            await _unitOfWork.GetRepository<Proposal>().UpdateAsync(proposal, id);
            await _unitOfWork.Commit();
            return await Result.FailAsync("Closed Proposal Successfully");
        }
        catch (Exception e)
        {
            return await Result.FailAsync(e.Message);
        }
    }

    public async Task<IResult> CancelProposal(string id, string userId)
    {
        try
        {
            var proposal = await _unitOfWork.GetRepository<Proposal>().GetByIdAsync(id);
            if (proposal.ProposerId != userId)
                throw new Exception("Cannot cancel proposal which you didn't create");
            proposal.Status = ProposalStatus.Closed;
            // TODO: Have to consider the reciprocators who were accepted initially
            await _unitOfWork.GetRepository<Proposal>().UpdateAsync(proposal, id);
            await _unitOfWork.Commit();
            return await Result.FailAsync("Cancelled Proposal Successfully");
        }
        catch (Exception e)
        {
            return await Result.FailAsync(e.Message);
        }
    }
}