using System.Collections.Generic;
using System.Linq;
using Spectacular.Extensions;
using Spectacular.SpecificationOperations;
using Spectacular.UnitTest.Models;
using Spectacular.UnitTest.Specs;
using Xunit;

namespace Spectacular.UnitTest
{
    public class CollectionExtension_Test
    {
        private readonly IReadOnlyList<Person> _personList = new List<Person>()
                                                             {
                                                                 new("adem", Genders.Male),
                                                                 new("elif", Genders.Female)
                                                             };

        [Fact]
        public void EnumerableExtensionTest()
        {
            AbstractSpecification<Person> spec = GenderShould.BeFemale;

            List<Person> filteredCollection = _personList.Where(spec)
                                                         .ToList();

            Assert.Single(filteredCollection);
            Assert.Equal("elif", filteredCollection.First().Name);
        }

        [Fact]
        public void EnumerableExtensionTest_NotMatch()
        {
            AbstractSpecification<Person> spec = NameShould.Contain("adem")
                                                           .And(GenderShould.BeFemale);

            List<Person> filteredCollection = _personList.Where(spec)
                                                         .ToList();

            Assert.Empty(filteredCollection);
        }


        [Fact]
        public void QueryableExtensionTest()
        {
            IQueryable<Person> persons = _personList.AsQueryable();

            AbstractSpecification<Person> spec = GenderShould.BeMale;

            List<Person> resultList = persons.Where(spec)
                                             .ToList();

            Assert.Single(resultList);
            Assert.Equal("adem", persons.First().Name);
        }

        [Fact]
        public void QueryableExtensionTest_NotMatch()
        {
            IQueryable<Person> persons = _personList.AsQueryable();

            AbstractSpecification<Person> spec = GenderShould.BeMale;
            spec = spec.And(NameShould.Contain("elif"));

            List<Person> resultList = persons.Where(spec)
                                             .ToList();

            Assert.Empty(resultList);
        }
    }
}