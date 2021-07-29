using System.Collections.Generic;
using System.Threading.Tasks;
using Spectacular;
using UserManagement.Api.Models;

namespace UserManagement.Api.Db.Repo
{
    public interface IUserRepository
    {
        public Task<IEnumerable<User>> GetUsersAsync(AbstractSpecification<User> userSpecs);
    }
}