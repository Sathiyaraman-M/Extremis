using Extremis.Wrapper;

namespace Extremis.Projects;

public interface IProjectService
{
    Task<IResult> CreateProject(CreateProjectDto request, string userName, string userId);
}