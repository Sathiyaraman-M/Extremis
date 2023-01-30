using System.Linq.Expressions;
using Extremis.Contracts;

namespace Extremis.Specifications.Base;

public class AndSpecification<T> : BaseSpecification<T> where T: class, IEntity
{
    private readonly ISpecification<T> _left;
    private readonly ISpecification<T> _right;

    public AndSpecification(ISpecification<T> left, ISpecification<T> right)
    {
        _left = left;
        _right = right;
    }

    public Expression<Func<T, bool>> GetFilterExpression()
    {
        var leftExpression = _left.FilterCondition;
        var rightExpression = _right.FilterCondition;

        var paramExpr = Expression.Parameter(typeof(T));
        var exprBody = Expression.AndAlso(leftExpression.Body, rightExpression.Body);
        exprBody = (BinaryExpression)new ParameterReplacer(paramExpr).Visit(exprBody);
        var finalExpr = Expression.Lambda<Func<T, bool>>(exprBody, paramExpr);
        return finalExpr;
    }

    public override Expression<Func<T, bool>> FilterCondition => GetFilterExpression();
}

public class OrSpecification<T> : BaseSpecification<T> where T: class, IEntity
{
    private readonly ISpecification<T> _left;
    private readonly ISpecification<T> _right;

    public OrSpecification(ISpecification<T> left, ISpecification<T> right)
    {
        _left = left;
        _right = right;
    }

    public Expression<Func<T, bool>> GetFilterExpression()
    {
        var leftExpression = _left.FilterCondition;
        var rightExpression = _right.FilterCondition;

        var paramExpr = Expression.Parameter(typeof(T));
        var exprBody = Expression.OrElse(leftExpression.Body, rightExpression.Body);
        exprBody = (BinaryExpression)new ParameterReplacer(paramExpr).Visit(exprBody);
        var finalExpr = Expression.Lambda<Func<T, bool>>(exprBody, paramExpr);
        return finalExpr;
    }

    public override Expression<Func<T, bool>> FilterCondition => GetFilterExpression();
}

public class ParameterReplacer : ExpressionVisitor
{
    private readonly ParameterExpression _parameter;

    protected override Expression VisitParameter(ParameterExpression node)
        => base.VisitParameter(_parameter);

    internal ParameterReplacer(ParameterExpression parameter)
    {
        _parameter = parameter;
    }
}