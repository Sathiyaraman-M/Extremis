using Extremis.Contracts;
using Extremis.Users;

namespace Extremis.Servers;

public class ServerMember : IEntity<string>
{
    public string Id { get; set; }
    public AppUser User { get; set; }
    public string UserId { get; set; }
    public Server Server { get; set; }
    public string ServerId { get; set; }
}