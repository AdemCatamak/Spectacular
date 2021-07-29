using System;
using System.Linq.Expressions;

namespace Spectacular
{
    public class DynamicSpecification<TModel> : AbstractSpecification<TModel>
    {
        public DynamicSpecification(Expression<Func<TModel, bool>> expression) : base(expression)
        {
        }
    }
}