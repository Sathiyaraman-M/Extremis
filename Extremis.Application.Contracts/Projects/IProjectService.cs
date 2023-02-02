using Extremis.Wrapper;

namespace Extremis.Projects;

public interface IProjectService
{
    Task<PaginatedResult<ProjectDto>> GetAllMyProjects(int pageNumber, int pageSize, string userId);
    Task<PaginatedResult<ProjectDto>> GetAllJoinedProjects(int pageNumber, int pageSize, string userId);
    Task<IResult> CreateProject(CreateProjectRequestDto request, string userName, string userId);
}