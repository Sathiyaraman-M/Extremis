using Extremis.Users;

namespace Extremis.Projects;

public class ProjectMemberDto
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string FullName { get; set; }
    public UserStatus Status { get; set; }
    public string CustomStatus { get; set; }
}