using System.Collections.Generic;
using System.Linq;
using Spectacular.Extensions;
using Spectacular.UnitTest.Models;
using Spectacular.UnitTest.Specs;
using Xunit;

namespace Spectacular.UnitTest
{
    public class SpecificationGroupConverterExtension_Test
    {
        private readonly IReadOnlyList<Person> _personList = new List<Person>()
                                                             {
                                                                 new("adem", Genders.Male),
                                                                 new("elif", Genders.Female)
                                                             };

        [Fact]
        public void When_AsSpecificationGroupExecuted__ThereShouldBe1SpecificationInGroup()
        {
            SpecificationGroup<Person> specs = GenderShould.BeFemale.AsSpecificationGroup();

            Assert.Single(specs.AbstractSpecifications);

            IEnumerable<Person> filteredPersons = _personList.Where(specs)
                                                             .ToList();

            Assert.Single(filteredPersons);
            Assert.Equal("elif", filteredPersons.First().Name);
        }
    }
}