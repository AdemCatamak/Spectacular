using System;
using System.Linq.Expressions;
using Spectacular;

namespace UserManagement.Api.Models.UserSpecs
{
    public class GenderShould : AbstractSpecification<User>
    {
        private GenderShould(Expression<Func<User, bool>> expression) : base(expression)
        {
        }

        public static AbstractSpecification<User> Be(Genders gender)
        {
            var spec = new GenderShould(user => user.Gender == gender);
            return spec;
        }
    }
}