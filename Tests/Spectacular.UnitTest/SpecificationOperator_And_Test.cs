using Spectacular.SpecificationOperations;
using Spectacular.UnitTest.Models;
using Spectacular.UnitTest.Specs;
using Xunit;

namespace Spectacular.UnitTest
{
    public class SpecificationOperator_And_Test
    {
        [Theory]
        [InlineData("adem", Genders.Male, true)]
        [InlineData("not-name", Genders.Male, false)]
        [InlineData("adem", Genders.Female, false)]
        [InlineData("not-name", Genders.Female, false)]
        public void AndOperatorInputs(string partialName, Genders genderInput, bool expectedResult)
        {
            Person person = new("adem", Genders.Male);

            AbstractSpecification<Person> specification = NameShould.Contain(partialName)
                                                                    .And(GenderShould.Be(genderInput));

            bool isSatisfied = specification.IsSatisfiedBy(person);

            Assert.Equal(expectedResult, isSatisfied);
        }
    }
}