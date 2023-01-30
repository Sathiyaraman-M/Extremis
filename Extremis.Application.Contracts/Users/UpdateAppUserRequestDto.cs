namespace Extremis.Users;

public class UpdateAppUserRequestDto
{
    public string Id { get; set; }
    
    public string UserName { get; set; }
    public string FullName { get; set; }
    
    public DateTime DateOfBirth { get; set; }
    
    public string City { get; set; }
    public string Country { get; set; }
    
    public string SingleLineDescription { get; set; }
    public string Bio { get; set; }
}