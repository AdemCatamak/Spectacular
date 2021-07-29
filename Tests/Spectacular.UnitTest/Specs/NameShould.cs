using System;
using System.Linq.Expressions;
using Spectacular.UnitTest.Models;

namespace Spectacular.UnitTest.Specs
{
    public class NameShould : AbstractSpecification<Person>
    {
        private NameShould(Expression<Func<Person, bool>> expression) : base(expression)
        {
        }

        public static AbstractSpecification<Person> Contain(string partialName)
        {
            NameShould nameShould = new(person => person.Name.Contains(partialName));
            return nameShould;
        }
    }
}