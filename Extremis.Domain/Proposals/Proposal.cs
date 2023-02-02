using Extremis.Contracts;
using Extremis.Projects;
using Extremis.Users;

namespace Extremis.Proposals;

public class Proposal : IAuditableEntity<string>
{
    public string Id { get; set; }
    
    public AppUser Proposer { get; set; }
    public string ProposerId { get; set; }
    
    public Project Project { get; set; }
    public string ProjectId { get; set; }
    
    public string Title { get; set; }
    public string Description { get; set; }
    
    public string Duration { get; set; }

    public string CreatedBy { get; set; }
    public string CreatedByUserId { get; set; }
    public DateTime CreatedOn { get; set; }
    public string LastModifiedBy { get; set; }
    public string LastModifiedByUserId { get; set; }
    public DateTime? LastModifiedOn { get; set; }
}