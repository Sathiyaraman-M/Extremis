using Extremis.Contracts;
using Extremis.Projects;
using Extremis.Users;

namespace Extremis.ProjectChats;

public class ChatMessage : IEntity<string>
{
    public string Id { get; set; }
    
    public string Message { get; set; }
    
    public AppUser FromUser { get; set; }
    public string FromUserId { get; set; }
    
    public AppUser ToUser { get; set; }
    public string ToUserId { get; set; }
    
    public Project Project { get; set; }
    public string ProjectId { get; set; }
    
    public DateTime CreateDateTime { get; set; }
}