using Extremis.Contracts;
using Extremis.Users;

namespace Extremis.Servers;

public class Server : IEntity<string>
{
    public string Id { get; set; }
    
    public string Name { get; set; }
    
    public IEnumerable<ServerMember> Members { get; set; }
    
    public AppUser Owner { get; set; }
    public string OwnerId { get; set; }
    
}