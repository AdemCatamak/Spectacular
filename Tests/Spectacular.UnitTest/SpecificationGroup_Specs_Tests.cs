using System.Collections.Generic;
using System.Linq;
using Spectacular.Extensions;
using Spectacular.SpecificationOperations;
using Spectacular.UnitTest.Models;
using Spectacular.UnitTest.Specs;
using Xunit;

namespace Spectacular.UnitTest
{
    public class SpecificationGroup_Specs_Tests
    {
        private readonly IReadOnlyList<Person> _personList = new List<Person>()
                                                             {
                                                                 new("adem", Genders.Male),
                                                                 new("elif", Genders.Female)
                                                             };

        [Fact]
        public void When_AndOperatorExecuted_Twice__ThereShouldBe3Specifications()
        {
            AbstractSpecification<Person> spec = GenderShould.BeMale;
            SpecificationGroup<Person> specificationGroup1 = spec.And(NameShould.Contain("a"));
            var specificationGroup2 = specificationGroup1.And(NameShould.Contain("e"));

            Assert.NotNull(specificationGroup2);
            Assert.Equal(3, specificationGroup2!.AbstractSpecifications.Count);

            List<Person> filteredCollection = _personList.Where(specificationGroup2)
                                                         .ToList();

            Assert.Single(filteredCollection);
            Assert.Equal("adem", filteredCollection.First().Name);
        }
    }
}