using Extremis.Proposals;

namespace Extremis.Controllers;

[Route("api/proposals")]
[ApiController]
public class ProposalController : ControllerBase
{
    private readonly IProposalService _proposalService;

    public ProposalController(IProposalService proposalService) => _proposalService = proposalService;

    [HttpGet("mine")]
    public async Task<IActionResult> GetAllMyProposals(int pageNumber, int pageSize, string searchString,
        string orderBy = null)
    {
        var userId = HttpContext.User.FindFirstValue(JwtClaimTypes.Subject);
        return Ok(await _proposalService.GetAllMyProposals(pageNumber, pageSize, userId));
    }
    [HttpGet]
    public async Task<IActionResult> GetAllProposals(int pageNumber, int pageSize, string searchString,
        string orderBy = null)
    {
        var userId = HttpContext.User.FindFirstValue(JwtClaimTypes.Subject);
        return Ok(await _proposalService.GetAllProposals(pageNumber, pageSize, searchString, orderBy, userId));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProposal(string id)
    {
        return Ok(await _proposalService.GetProposal(id));
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateProposal(CreateProposalRequestDto request)
    {
        var userId = HttpContext.User.FindFirstValue(JwtClaimTypes.Subject);
        var userName = HttpContext.User.FindFirstValue(JwtClaimTypes.PreferredUserName);
        return Ok(await _proposalService.CreateProposal(request, userName, userId));
    }
}