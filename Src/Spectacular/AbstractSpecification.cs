using System;
using System.Linq.Expressions;

namespace Spectacular
{
    public abstract class AbstractSpecification<TModel>
    {
        public Expression<Func<TModel, bool>> Expression { get; }

        private Func<TModel, bool>? _expressionFunc;
        private Func<TModel, bool> ExpressionFunc => _expressionFunc ??= Expression.Compile();

        protected AbstractSpecification(Expression<Func<TModel, bool>> expression)
        {
            Expression = expression;
        }

        public bool IsSatisfiedBy(TModel obj)
        {
            bool result = ExpressionFunc(obj);
            return result;
        }

        public static AbstractSpecification<TModel> Default => new DynamicSpecification<TModel>(_ => true);
    }
}