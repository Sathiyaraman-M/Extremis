namespace Extremis.Users;

public class UpdateUserStatusRequestDto
{
    public string Id { get; set; }
    public UserStatus Status { get; set; }
    public string CustomStatus { get; set; }
}