using System;
using System.Linq.Expressions;
using Spectacular;

namespace UserManagement.Api.Models.UserSpecs
{
    public class UsernameShould : AbstractSpecification<User>
    {
        private UsernameShould(Expression<Func<User, bool>> expression) : base(expression)
        {
        }

        public static AbstractSpecification<User> Be(string username)
        {
            UsernameShould spec = new(user => user.Username == username);
            return spec;
        }

        public static AbstractSpecification<User> Contain(string partialUsername)
        {
            UsernameShould spec = new(user => user.Username.Contains(partialUsername));
            return spec;
        }
    }
}