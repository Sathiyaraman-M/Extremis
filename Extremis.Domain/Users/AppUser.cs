using Extremis.Contracts;
using Extremis.ProjectChats;
using Microsoft.AspNetCore.Identity;

namespace Extremis.Users;

public class AppUser : IdentityUser, IAuditableEntity<string>
{
    public AppUser()
    {
        ChatMessagesFromUsers = new HashSet<ChatMessage>();
        ChatMessagesToUsers = new HashSet<ChatMessage>();
    }
    
    public string FullName { get; set; }
    public DateTime DateOfBirth { get; set; }
    
    public string City { get; set; }
    public string Country { get; set; }
    public string ProfilePictureDataUrl { get; set; }
    
    public string SingleLineDescription { get; set; }
    public string Bio { get; set; }
    
    public UserStatus Status { get; set; }
    public string CustomStatus { get; set; }
    public bool IsActive { get; set; }
    
    public ICollection<ChatMessage> ChatMessagesFromUsers { get; set; }
    public ICollection<ChatMessage> ChatMessagesToUsers { get; set; }

    public string CreatedBy { get; set; }
    public string CreatedByUserId { get; set; }
    public DateTime CreatedOn { get; set; }
    public string LastModifiedBy { get; set; }
    public string LastModifiedByUserId { get; set; }
    public DateTime? LastModifiedOn { get; set; }
    public bool IsDeleted { get; set; }
}