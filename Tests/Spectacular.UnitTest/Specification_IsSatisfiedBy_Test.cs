using Spectacular.UnitTest.Models;
using Spectacular.UnitTest.Specs;
using Xunit;

namespace Spectacular.UnitTest
{
    public class Specification_IsSatisfiedBy_Test
    {
        [Fact]
        public void WhenDefaultSpec__ResponseShouldBeAlwaysTrue()
        {
            Person person = new("");

            AbstractSpecification<Person> spec = AbstractSpecification<Person>.Default;
            bool isSatisfied = spec.IsSatisfiedBy(person);

            Assert.True(isSatisfied);
        }

        [Fact]
        public void WhenCriteriaDoesMet__ResponseShouldBeTrue()
        {
            Person person = new("adem");

            AbstractSpecification<Person> spec = NameShould.Contain("de");
            bool isSatisfied = spec.IsSatisfiedBy(person);

            Assert.True(isSatisfied);
        }

        [Fact]
        public void WhenCriteriaDoesNotMet__ResponseShouldBeFalse()
        {
            Person person = new("adem");

            AbstractSpecification<Person> spec = NameShould.Contain("name-not-contains-this");
            bool isSatisfied = spec.IsSatisfiedBy(person);

            Assert.False(isSatisfied);
        }
    }
}