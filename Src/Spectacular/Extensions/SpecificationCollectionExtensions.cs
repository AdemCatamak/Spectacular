using System.Collections.Generic;
using System.Linq;

namespace Spectacular.Extensions
{
    public static class SpecificationCollectionExtensions
    {
        public static IEnumerable<TModel> Where<TModel>(this IEnumerable<TModel> models,
                                                        AbstractSpecification<TModel> spec)
        {
            models = models.Where(spec.IsSatisfiedBy);
            return models;
        }

        public static IQueryable<TModel> Where<TModel>(this IQueryable<TModel> models,
                                                       AbstractSpecification<TModel> spec)
        {
            models = models.Where(spec.Expression);
            return models;
        }
    }
}