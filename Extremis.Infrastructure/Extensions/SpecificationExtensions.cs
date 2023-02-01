using Extremis.Contracts;
using Extremis.Specifications.Base;
using Microsoft.EntityFrameworkCore;

namespace Extremis.Extensions;

public static class SpecificationExtensions
{
    public static IQueryable<T> Specify<T>(this IQueryable<T> query, ISpecification<T> spec) where T : class, IEntity
    {
        if (spec == null) 
            return query;
        
        if (spec.FilterCondition != null)
            return query.Where(spec.FilterCondition);

        query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

        query = spec.IncludeStrings.Aggregate(query, (current, include) => current.Include(include));

        return query;
    }
    
    public static ISpecification<T> And<T>(this ISpecification<T> left, ISpecification<T> right) where T : class, IEntity
    {
        return new AndSpecification<T>(left, right);
    }

    public static ISpecification<T> Or<T>(this ISpecification<T> left, ISpecification<T> right) where T : class, IEntity
    {
        return new OrSpecification<T>(left, right);
    }
}