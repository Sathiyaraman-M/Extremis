using Extremis.Contracts;
using Extremis.Users;

namespace Extremis.Projects;

public class Project : IAuditableEntity<string>
{
    public string Id { get; set; }
    
    public AppUser Owner { get; set; }
    public string OwnerId { get; set; }
    
    public string Title { get; set; }
    public string Description { get; set; }
    
    public List<ProjectMember> Members { get; set; }

    public string CreatedBy { get; set; }
    public string CreatedByUserId { get; set; }
    public DateTime CreatedOn { get; set; }
    public string LastModifiedBy { get; set; }
    public string LastModifiedByUserId { get; set; }
    public DateTime? LastModifiedOn { get; set; }
}