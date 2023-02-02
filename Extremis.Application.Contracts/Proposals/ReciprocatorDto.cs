namespace Extremis.Proposals;

public class ReciprocatorDto
{
    public string Id { get; set; }
    public string ReciprocatorId { get; set; }
    public string ReciprocatorName { get; set; }
    public string ProposalId { get; set; }
    public string Note { get; set; }

    public string CreatedBy { get; set; }
    public string CreatedByUserId { get; set; }
    public DateTime CreatedOn { get; set; }
}