using Extremis.Proposals;
using Extremis.Specifications.Base;

namespace Extremis.Specifications;

public class ProposalSearchSpecification : BaseSpecification<Proposal>
{
    public ProposalSearchSpecification(string searchString)
    {
        if (string.IsNullOrEmpty(searchString))
        {
            FilterCondition = p => true;
        }
        else
        {
            searchString = searchString.ToLower();
            FilterCondition = p =>
                p.Title.ToLower().Contains(searchString) || p.Description.ToLower().Contains((searchString));
        }
    }
}