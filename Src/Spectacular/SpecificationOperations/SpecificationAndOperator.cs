using Spectacular.ExpressionOperations;

namespace Spectacular.SpecificationOperations
{
    public static class ExpressionSpecificationAndOperatorExtension
    {
        public static SpecificationGroup<T> And<T>(this AbstractSpecification<T> specificationLeft, AbstractSpecification<T> specificationRight)
        {
            var specificationAndOperator = new ExpressionAndOperator();
            SpecificationGroup<T> specificationGroup = new(specificationLeft, specificationRight, specificationAndOperator);
            return specificationGroup;
        }

        public static SpecificationGroup<T> And<T>(this SpecificationGroup<T> specificationGroupLeft, SpecificationGroup<T> specificationGroupRight)
        {
            var specificationAndOperator = new ExpressionAndOperator();
            SpecificationGroup<T> result = new(specificationGroupLeft, specificationGroupRight, specificationAndOperator);
            return result;
        }

        public static SpecificationGroup<T> And<T>(this SpecificationGroup<T> specificationGroup, AbstractSpecification<T> specification)
        {
            var specificationAndOperator = new ExpressionAndOperator();
            SpecificationGroup<T> result = new(specificationGroup, specification, specificationAndOperator);
            return result;
        }
    }
}