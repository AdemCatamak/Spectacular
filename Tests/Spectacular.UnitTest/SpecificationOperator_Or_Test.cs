using Spectacular.SpecificationOperations;
using Spectacular.UnitTest.Models;
using Spectacular.UnitTest.Specs;
using Xunit;

namespace Spectacular.UnitTest
{
    public class SpecificationOperator_Or_Test
    {
        [Theory]
        [InlineData("adem", Genders.Female, true)]
        [InlineData("adem", Genders.Male, true)]
        [InlineData("not-name", Genders.Male, true)]
        [InlineData("not-name", Genders.Female, false)]
        public void OrOperatorInputs(string partialName, Genders genderInput, bool expectedResult)
        {
            Person person = new("adem", Genders.Male);

            AbstractSpecification<Person> specification = NameShould.Contain(partialName)
                                                                    .Or(GenderShould.Be(genderInput));

            bool isSatisfied = specification.IsSatisfiedBy(person);

            Assert.Equal(expectedResult, isSatisfied);
        }
    }
}