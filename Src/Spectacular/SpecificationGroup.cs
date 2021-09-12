using System.Collections.Generic;
using Spectacular.ExpressionOperations;

namespace Spectacular
{
    public class SpecificationGroup<T> : AbstractSpecification<T>
    {
        public IReadOnlyCollection<AbstractSpecification<T>> AbstractSpecifications => _specifications;

        private readonly List<AbstractSpecification<T>> _specifications;

        public SpecificationGroup(AbstractSpecification<T> abstractSpecificationLeft,
                                  AbstractSpecification<T> abstractSpecificationRight,
                                  IExpressionCombineOperator expressionCombineOperator)
            : base(expressionCombineOperator.Combine(abstractSpecificationLeft.Expression, abstractSpecificationRight.Expression))
        {
            _specifications = new List<AbstractSpecification<T>>
                              {
                                  abstractSpecificationLeft,
                                  abstractSpecificationRight
                              };
        }

        public SpecificationGroup(SpecificationGroup<T> specificationGroup,
                                  AbstractSpecification<T> abstractSpecification,
                                  IExpressionCombineOperator expressionCombineOperator)
            : base(expressionCombineOperator.Combine(specificationGroup.Expression, abstractSpecification.Expression))
        {
            _specifications = new List<AbstractSpecification<T>>(specificationGroup._specifications)
                              {
                                  abstractSpecification
                              };
        }

        public SpecificationGroup(SpecificationGroup<T> specificationGroupLeft,
                                  SpecificationGroup<T> specificationGroupRight,
                                  IExpressionCombineOperator expressionCombineOperator)
            : base(expressionCombineOperator.Combine(specificationGroupLeft.Expression, specificationGroupRight.Expression))
        {
            _specifications = new List<AbstractSpecification<T>>(specificationGroupLeft._specifications);
            _specifications.AddRange(specificationGroupRight._specifications);
        }
    }
}