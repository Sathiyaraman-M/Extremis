using Extremis.Users;

namespace Extremis.Projects;

public class ProjectFullDto
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string OwnerId { get; set; }
    public string OwnerName { get; set; }
    
    public UserStatus OwnerStatus { get; set; }
    
    public string OwnerCustomStatus { get; set; }
    public List<ProjectMemberDto> Members { get; set; } = new();
}