using Extremis.Repositories;
using Extremis.Wrapper;

namespace Extremis.Projects;

public class ProjectService : IProjectService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProjectService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IResult> CreateProject(CreateProjectDto request, string userName, string userId)
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
}