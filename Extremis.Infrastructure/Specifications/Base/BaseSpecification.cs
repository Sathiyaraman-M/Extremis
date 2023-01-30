using System.Linq.Expressions;
using Extremis.Contracts;

namespace Extremis.Specifications.Base;

public abstract class BaseSpecification<T> : ISpecification<T> where T : class, IEntity
{
    public virtual Expression<Func<T, bool>> FilterCondition { get; protected set; }

    public List<Expression<Func<T, object>>> Includes { get; } = new();

    public List<string> IncludeStrings { get; } = new();

    protected virtual void AddInclude(Expression<Func<T, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }

    protected virtual void AddInclude(string includeString)
    {
        IncludeStrings.Add(includeString);
    }
}