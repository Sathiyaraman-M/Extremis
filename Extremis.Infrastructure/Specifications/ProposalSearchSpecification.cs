using Extremis.Proposals;
using Extremis.Specifications.Base;

namespace Extremis.Specifications;

public class ProposalSearchSpecification : BaseSpecification<Proposal>
{
    public ProposalSearchSpecification(string searchString)
    {
        searchString = searchString.ToLower();
        if (string.IsNullOrWhiteSpace(searchString))
        {
            FilterCondition = p => true;
        }
        else
        {
            FilterCondition = p =>
                p.Title.ToLower().Contains(searchString) || p.Description.ToLower().Contains((searchString));
        }
    }
}