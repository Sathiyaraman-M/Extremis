namespace Extremis.Proposals;

public class CreateProposalRequestDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    
    public string Duration { get; set; }

    public DateTime ExpireTime { get; set; }
}