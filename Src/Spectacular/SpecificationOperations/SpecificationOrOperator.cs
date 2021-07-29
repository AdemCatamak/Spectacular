using System;
using System.Linq;
using System.Linq.Expressions;

namespace Spectacular.SpecificationOperations
{
    internal class SpecificationCombineOrOperator : ISpecificationCombineOperator
    {
        // https://stackoverflow.com/questions/457316/combining-two-expressions-expressionfunct-bool
        public AbstractSpecification<TModel> Combine<TModel>(AbstractSpecification<TModel> left, AbstractSpecification<TModel> right)
        {
            Expression<Func<TModel, bool>> resultExpression;
            var param = left.Expression.Parameters.First();
            if (ReferenceEquals(param, right.Expression.Parameters.First()))
            {
                resultExpression = Expression.Lambda<Func<TModel, bool>>(
                                                                         Expression.OrElse(left.Expression.Body, right.Expression.Body), param);
            }
            else
            {
                resultExpression = Expression.Lambda<Func<TModel, bool>>(
                                                                         Expression.OrElse(
                                                                                           left.Expression.Body,
                                                                                           Expression.Invoke(right.Expression, param)), param);
            }

            var combinedSpecification = new DynamicSpecification<TModel>(resultExpression);
            return combinedSpecification;
        }
    }

    public static class ExpressionSpecificationOrOperatorExtension
    {
        public static AbstractSpecification<T> Or<T>(this AbstractSpecification<T> specificationLeft, AbstractSpecification<T> specificationRight)
        {
            var specificationOrOperator = new SpecificationCombineOrOperator();
            AbstractSpecification<T> specification = specificationOrOperator.Combine(specificationLeft, specificationRight);
            return specification;
        }
    }
}