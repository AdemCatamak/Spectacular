using System;
using System.Linq.Expressions;
using Spectacular;

namespace UserManagement.Api.Models.UserSpecs
{
    public class UserIdShould : AbstractSpecification<User>
    {
        private UserIdShould(Expression<Func<User, bool>> expression) : base(expression)
        {
        }

        public static AbstractSpecification<User> Be(Guid id)
        {
            var spec = new UserIdShould(user => user.Id == id);
            return spec;
        }
    }
}