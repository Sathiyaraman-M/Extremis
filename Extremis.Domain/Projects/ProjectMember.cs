using Extremis.Contracts;
using Extremis.Users;

namespace Extremis.Projects;

public class ProjectMember : IEntity<string>
{
    public string Id { get; set; }
    
    public Project Project { get; set; }
    public string ProjectId { get; set; }
    
    public AppUser Member { get; set; }
    public string MemberId { get; set; }
}