namespace Extremis.ProjectChats;

public class SendMessageDto
{
    public string ProjectId { get; set; }
    public string Message { get; set; }
    public string ToUserId { get; set; }
}