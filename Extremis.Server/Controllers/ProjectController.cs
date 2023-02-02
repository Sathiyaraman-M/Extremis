using Extremis.Projects;

namespace Extremis.Controllers;

[Route("api/projects")]
[ApiController]
public class ProjectController : Controller
{
    private readonly IProjectService _projectService;

    public ProjectController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProject(CreateProjectDto request)
    {
        var userId = HttpContext.User.FindFirstValue(JwtClaimTypes.Subject);
        var userName = HttpContext.User.FindFirstValue(JwtClaimTypes.PreferredUserName);
        return Ok(await _projectService.CreateProject(request, userName, userId));
    }
}