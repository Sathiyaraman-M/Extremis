﻿namespace Extremis.Proposals;

public class ProposalDto
{
    public string Id { get; set; }
    public string ProposerId { get; set; }
    public string ProposerName { get; set; }
    
    public string Title { get; set; }
    public string Description { get; set; }
    
    public ProposalStatus Status { get; set; }
    public string Duration { get; set; }
    
    public DateTime CreatedOn { get; set; }
    public DateTime? LastModifiedOn { get; set; }
    public DateTime ExpireTime { get; set; }
}