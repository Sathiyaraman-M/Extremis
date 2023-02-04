namespace Extremis.ProjectChats;

public class MessageDto
{
    public string Id { get; set; }
    public string Message { get; set; }
    public DateTime CreateDateTime { get; set; }
    public string FromUserId { get; set; }
    public string FromUserName { get; set; }
    public string ToUserId { get; set; }
    public string ToUserName { get; set; }
    public string ProjectId { get; set; }
}