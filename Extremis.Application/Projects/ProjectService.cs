using System.Linq.Expressions;
using Extremis.Extensions;
using Extremis.Proposals;
using Extremis.Repositories;
using Extremis.Users;
using Extremis.Wrapper;
using Microsoft.EntityFrameworkCore;

namespace Extremis.Projects;

public class ProjectService : IProjectService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProjectService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PaginatedResult<ProjectDto>> GetAllMyProjects(int pageNumber, int pageSize, string userId)
    {
        try
        {
            Expression<Func<Project, ProjectDto>> expression = project => new ProjectDto()
            {
                Id = project.Id,
                Description = project.Description,
                MembersCount = project.Members.Count,
                OwnerName = project.Owner.FullName,
                Title = project.Title
            };
            return await _unitOfWork.GetRepository<Project>().Entities
                .Include(x => x.Owner)
                .Include(x => x.Members)
                .Where(x => x.OwnerId == userId)
                .Select(expression)
                .ToPaginatedListAsync(pageNumber, pageSize);
        }
        catch (Exception e)
        {
            return PaginatedResult<ProjectDto>.Failure(new List<string>() { e.Message });
        }
    }

    public async Task<PaginatedResult<ProjectDto>> GetAllJoinedProjects(int pageNumber, int pageSize, string userId)
    {
        try
        {
            Expression<Func<Project, ProjectDto>> expression = project => new ProjectDto()
            {
                Id = project.Id,
                Description = project.Description,
                MembersCount = project.Members.Count,
                OwnerName = project.Owner.FullName,
                Title = project.Title
            };
            return await _unitOfWork.GetRepository<Project>().Entities
                .Include(x => x.Owner)
                .Include(x => x.Members)
                .Where(x => x.Members.Any(y => y.MemberId == userId))
                .Select(expression)
                .ToPaginatedListAsync(pageNumber, pageSize);
        }
        catch (Exception e)
        {
            return PaginatedResult<ProjectDto>.Failure(new List<string>() { e.Message });
        }
    }

    public async Task<IResult<ProjectFullDto>> GetProjectFullInfo(string id, string userId)
    {
        try
        {
            var project = await _unitOfWork.GetRepository<Project>().Entities
                .Include(x => x.Owner)
                .Include(x => x.Members)
                .ThenInclude(x => x.Member)
                .Where(x => x.OwnerId == userId || x.Members.Any(y => y.MemberId == userId))
                .Select(x => new ProjectFullDto()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    OwnerId = x.OwnerId,
                    OwnerName = x.Owner.UserName,
                    OwnerFullName = x.Owner.FullName,
                    OwnerStatus = x.Owner.Status,
                    OwnerCustomStatus = x.Owner.CustomStatus,
                    Members = x.Members.Select(z => z.Member).Select(y => new ProjectMemberDto()
                    {
                        Id = y.Id,
                        UserName = y.UserName,
                        FullName = y.FullName,
                        Status = y.Status,
                        CustomStatus = y.CustomStatus
                    }).ToList()
                })
                .FirstOrDefaultAsync(x => x.Id == id);
            return await Result<ProjectFullDto>.SuccessAsync(project);
        }
        catch (Exception e)
        {
            return await Result<ProjectFullDto>.FailAsync(e.Message);
        }
    }

    public async Task<IResult<List<(string, string)>>> GetAllProjectsList(string userId)
    {
        try
        {
            var projects = await _unitOfWork.GetRepository<Project>().Entities
                .Include(x => x.Members)
                .Where(x => x.OwnerId == userId || x.Members.Any(y => y.MemberId == userId))
                .Select(x => new { x.Id, x.Title })
                .ToListAsync();
            return await Result<List<(string, string)>>.SuccessAsync(projects.Select(x => (x.Id, x.Title)).ToList());
        }
        catch (Exception e)
        {
            return await Result<List<(string, string)>>.FailAsync(e.Message);
        }
    }

    public async Task<IResult> CreateProject(CreateProjectRequestDto request, string userName, string userId)
    {
        try
        {
            var project = new Project()
            {
                OwnerId = userId,
                Description = request.Description,
                Title = request.Title,
                CreatedByUserId = userId,
                CreatedBy = userName,
                CreatedOn = DateTime.Now,
                LastModifiedOn = DateTime.Now,
                LastModifiedByUserId = userId,
                LastModifiedBy = userName
            };
            await _unitOfWork.GetRepository<Project>().AddAsync(project);
            await _unitOfWork.Commit();
            return await Result.SuccessAsync("Created Project Successfully");
        }
        catch (Exception e)
        {
            return await Result.FailAsync(e.Message);
        }
    }

    public async Task<IResult> AddMember(string reciprocationId, string userId)
    {
        try
        {
            var reciprocation = await _unitOfWork.GetRepository<Reciprocation>().Entities
                .Include(x => x.Reciprocator)
                .Include(x => x.Proposal)
                .ThenInclude(x => x.Project)
                .ThenInclude(x => x.Members)
                .FirstOrDefaultAsync(x => x.Id == reciprocationId);
            if (reciprocation == null)
            {
                throw new Exception("Candidate Not Found!");
            }

            var project = await _unitOfWork.GetRepository<Project>().Entities
                .FirstOrDefaultAsync(x => x.Id == reciprocation.Proposal.ProjectId);
            var user = await _unitOfWork.GetRepository<AppUser>().Entities
                .FirstOrDefaultAsync(x => x.Id == reciprocation.ReciprocatorId);
            var newMember = new ProjectMember()
            {
                Id = Guid.NewGuid().ToString(),
                MemberId = user.Id,
                ProjectId = reciprocation.Proposal.ProjectId
            };
            project.Members.Add(newMember);
            await _unitOfWork.GetRepository<Project>().UpdateAsync(project, project.Id);
            await _unitOfWork.GetRepository<Reciprocation>().DeleteAsync(reciprocation);
            await _unitOfWork.Commit();
            return await Result.SuccessAsync("Created Project Successfully");
        }
        catch (Exception e)
        {
            return await Result.FailAsync(e.Message);
        }
    }
}