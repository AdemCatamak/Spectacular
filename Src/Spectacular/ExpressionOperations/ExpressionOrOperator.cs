using System;
using System.Linq;
using System.Linq.Expressions;

namespace Spectacular.ExpressionOperations
{
    internal class ExpressionOrOperator : IExpressionCombineOperator
    {
        // https://stackoverflow.com/questions/457316/combining-two-expressions-expressionfunct-bool
        public Expression<Func<TModel, bool>> Combine<TModel>(Expression<Func<TModel, bool>> left, Expression<Func<TModel, bool>> right)
        {
            Expression<Func<TModel, bool>> resultExpression;
            var param = left.Parameters.First();
            if (ReferenceEquals(param, right.Parameters.First()))
            {
                resultExpression = Expression.Lambda<Func<TModel, bool>>(
                                                                         Expression.OrElse(left.Body, right.Body), param);
            }
            else
            {
                resultExpression = Expression.Lambda<Func<TModel, bool>>(
                                                                         Expression.OrElse(
                                                                                           left.Body,
                                                                                           Expression.Invoke(right, param)), param);
            }

            return resultExpression;
        }
    }
}