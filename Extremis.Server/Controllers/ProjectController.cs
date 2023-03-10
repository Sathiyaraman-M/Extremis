using Extremis.Projects;

namespace Extremis.Controllers;

[Authorize]
[Route("api/projects")]
[ApiController]
public class ProjectController : Controller
{
    private readonly IProjectService _projectService;

    public ProjectController(IProjectService projectService) => _projectService = projectService;

    [HttpGet("own")]
    public async Task<IActionResult> GetAllMyProjects(int pageNumber, int pageSize)
    {
        var userId = HttpContext.User.FindFirstValue(JwtClaimTypes.Subject);
        return Ok(await _projectService.GetAllMyProjects(pageNumber, pageSize, userId));
    }

    [HttpGet("joined")]
    public async Task<IActionResult> GetAllJoinedProjects(int pageNumber, int pageSize)
    {
        var userId = HttpContext.User.FindFirstValue(JwtClaimTypes.Subject);
        return Ok(await _projectService.GetAllJoinedProjects(pageNumber, pageSize, userId));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProjectFullInfo(string id)
    {
        var userId = HttpContext.User.FindFirstValue(JwtClaimTypes.Subject);
        return Ok(await _projectService.GetProjectFullInfo(id, userId));
    }

    [HttpGet("list")]
    public async Task<IActionResult> GetAllProjectsList()
    {
        var userId = HttpContext.User.FindFirstValue(JwtClaimTypes.Subject);
        return Ok(await _projectService.GetAllProjectsList(userId));
    }

    [HttpPost]
    public async Task<IActionResult> CreateProject(CreateProjectRequestDto request)
    {
        var userId = HttpContext.User.FindFirstValue(JwtClaimTypes.Subject);
        var userName = HttpContext.User.FindFirstValue(JwtClaimTypes.PreferredUserName);
        return Ok(await _projectService.CreateProject(request, userName, userId));
    }
    
    [HttpGet("members/new")]
    public async Task<IActionResult> AddMember(string applicationId)
    {
        var userId = HttpContext.User.FindFirstValue(JwtClaimTypes.Subject);
        return Ok(await _projectService.AddMember(applicationId, userId));
    }
}