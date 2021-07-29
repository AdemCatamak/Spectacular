using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Spectacular;
using Spectacular.Extensions;
using UserManagement.Api.Models;

namespace UserManagement.Api.Db.Repo
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _userDbContext;

        public UserRepository(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }

        public async Task<IEnumerable<User>> GetUsersAsync(AbstractSpecification<User> userSpecs)
        {
            IQueryable<User> users = _userDbContext.Users
                                                   .Where(userSpecs);

            List<User> userList = await users.ToListAsync();
            return userList;
        }
    }
}