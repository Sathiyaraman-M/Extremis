using Extremis.Contracts;
using Extremis.Users;

namespace Extremis.Proposals;

public class Reciprocation : IAuditableEntity<string>
{
    public string Id { get; set; }
    
    public AppUser Reciprocator { get; set; }
    public string ReciprocatorId { get; set; }
    
    public Proposal Proposal { get; set; }
    public string ProposalId { get; set; }
    
    public string Note { get; set; }

    public string CreatedBy { get; set; }
    public string CreatedByUserId { get; set; }
    public DateTime CreatedOn { get; set; }
    public string LastModifiedBy { get; set; }
    public string LastModifiedByUserId { get; set; }
    public DateTime? LastModifiedOn { get; set; }
}