using Extremis.Proposals;
using Extremis.Specifications.Base;

namespace Extremis.Specifications;

public class ReciprocationSearchSpecification : BaseSpecification<Reciprocation>
{
    public ReciprocationSearchSpecification(string searchString)
    {
        if (string.IsNullOrEmpty(searchString))
        {
            FilterCondition = p => true;
        }
        else
        {
            searchString = searchString.ToLower();
            FilterCondition = p =>
                p.Proposal.Title.ToLower().Contains(searchString) || p.Proposal.Description.ToLower().Contains((searchString));
        }
    }
}