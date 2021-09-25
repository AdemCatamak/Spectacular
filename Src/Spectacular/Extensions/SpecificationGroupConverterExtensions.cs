namespace Spectacular.Extensions
{
    public static class SpecificationGroupConverterExtensions
    {
        public static SpecificationGroup<T> AsSpecificationGroup<T>(this AbstractSpecification<T> abstractSpecification)
        {
            SpecificationGroup<T> specificationGroup = new(abstractSpecification);
            return specificationGroup;
        }
    }
}