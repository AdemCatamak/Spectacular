namespace Spectacular.SpecificationOperations
{
    internal interface ISpecificationCombineOperator
    {
        AbstractSpecification<TModel> Combine<TModel>(AbstractSpecification<TModel> left, AbstractSpecification<TModel> right);
    }
}