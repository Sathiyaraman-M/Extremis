namespace Extremis.Proposals;

public class InitiateProposalRequestDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    
    public string Duration { get; set; }
    public string ProjectId { get; set; }
}