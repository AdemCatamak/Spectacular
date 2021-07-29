using Spectacular.UnitTest.Models;

namespace Spectacular.UnitTest.Specs
{
    public class GenderShould : AbstractSpecification<Person>
    {
        private GenderShould(Genders gender)
            : base(person => person.Gender == gender)
        {
        }

        public static AbstractSpecification<Person> Be(Genders gender)
        {
            GenderShould genderShould = new(gender);
            return genderShould;
        }

        public static AbstractSpecification<Person> BeMale => Be(Genders.Male);
        public static AbstractSpecification<Person> BeFemale => Be(Genders.Female);
    }
}