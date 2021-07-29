using System;
using System.Linq.Expressions;
using Spectacular;

namespace UserManagement.Api.Models.UserSpecs
{
    public class AgeShould : AbstractSpecification<User>
    {
        private AgeShould(Expression<Func<User, bool>> expression) : base(expression)
        {
        }

        public static AbstractSpecification<User> BeGreaterThan(int age)
        {
            DateTime now = DateTime.UtcNow;
            DateTime threshold = now.AddYears(-age);

            AgeShould spec = new AgeShould(user => user.Birthdate < threshold);
            return spec;
        }

        public static AbstractSpecification<User> BeLessThan(int age)
        {
            DateTime now = DateTime.UtcNow;
            DateTime threshold = now.AddYears(-age);

            AgeShould spec = new AgeShould(user => user.Birthdate > threshold);
            return spec;
        }
    }
}