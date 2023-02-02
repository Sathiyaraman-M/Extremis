using Extremis.Proposals;

namespace Extremis.Controllers;

[Authorize]
[Route("api/proposals")]
[ApiController]
public class ProposalController : ControllerBase
{
    private readonly IProposalService _proposalService;

    public ProposalController(IProposalService proposalService) => _proposalService = proposalService;
    
    [HttpGet("check-open")]
    public async Task<IActionResult> IsProposalPresent(string projectId)
    {
        return Ok(await _proposalService.IsProposalPresent(projectId));
    }
    
    [HttpPost("{id}")]
    public async Task<IActionResult> InitiateProposal(InitiateProposalRequestDto request)
    {
        var userId = HttpContext.User.FindFirstValue(JwtClaimTypes.Subject);
        var userName = HttpContext.User.FindFirstValue(JwtClaimTypes.PreferredUserName);
        return Ok(await _proposalService.InitiateProposal(request, userName, userId));
    }
    
    [HttpGet("close")]
    public async Task<IActionResult> CloseProposal(string projectId)
    {
        var userId = HttpContext.User.FindFirstValue(JwtClaimTypes.Subject);
        return Ok(await _proposalService.CloseProposal(projectId, userId));
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllProposals(int pageNumber, int pageSize, string searchString,
        string orderBy = null)
    {
        var userId = HttpContext.User.FindFirstValue(JwtClaimTypes.Subject);
        return Ok(await _proposalService.GetAllProposals(pageNumber, pageSize, searchString, orderBy, userId));
    }
    
    [HttpPost("apply")]
    public async Task<IActionResult> ApplyForProposal(ApplyForProposalRequestDto request)
    {
        var userId = HttpContext.User.FindFirstValue(JwtClaimTypes.Subject);
        var userName = HttpContext.User.FindFirstValue(JwtClaimTypes.PreferredUserName);
        return Ok(await _proposalService.ApplyForProposal(request, userName, userId));
    }
    
    [HttpGet("apply-check")]
    public async Task<IActionResult> CheckAppliedStatus(string id)
    {
        var userId = HttpContext.User.FindFirstValue(JwtClaimTypes.Subject);
        return Ok(await _proposalService.CheckApplyStatusProposal(id, userId));
    }
    
    [HttpGet("candidates")]
    public async Task<IActionResult> GetAllCandidates(int pageNumber, int pageSize, string searchString,
        string id)
    {
        var userId = HttpContext.User.FindFirstValue(JwtClaimTypes.Subject);
        return Ok(await _proposalService.GetAllCandidates(pageNumber, pageSize, searchString, id, userId));
    }

    // [HttpGet("mine")]
    // public async Task<IActionResult> GetAllMyProposals(int pageNumber, int pageSize, string searchString,
    //     string orderBy = null)
    // {
    //     var userId = HttpContext.User.FindFirstValue(JwtClaimTypes.Subject);
    //     return Ok(await _proposalService.GetAllMyProposals(pageNumber, pageSize, searchString, userId));
    // }
    
    // [HttpGet("{id}")]
    // public async Task<IActionResult> GetProposal(string id)
    // {
    //     return Ok(await _proposalService.GetProposal(id));
    // }
    //
    // [HttpPost("create")]
    // public async Task<IActionResult> CreateProposal(InitiateProposalRequestDto request)
    // {
    //     var userId = HttpContext.User.FindFirstValue(JwtClaimTypes.Subject);
    //     var userName = HttpContext.User.FindFirstValue(JwtClaimTypes.PreferredUserName);
    //     return Ok(await _proposalService.InitiateProposal(request, userName, userId));
    // }
    
    // [HttpGet("close")]
    // public async Task<IActionResult> CloseProposal(string id)
    // {
    //     var userId = HttpContext.User.FindFirstValue(JwtClaimTypes.Subject);
    //     return Ok(await _proposalService.CloseProposal(id, userId));
    // }
    //
    // [HttpGet("cancel")]
    // public async Task<IActionResult> CancelProposal(string id)
    // {
    //     var userId = HttpContext.User.FindFirstValue(JwtClaimTypes.Subject);
    //     return Ok(await _proposalService.CancelProposal(id, userId));
    // }
}