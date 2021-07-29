using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Spectacular;
using Spectacular.SpecificationOperations;
using UserManagement.Api.Db.Repo;
using UserManagement.Api.Models;
using UserManagement.Api.Models.UserSpecs;

namespace UserManagement.Api.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? username = null,
                                             [FromQuery] Genders? gender = null,
                                             [FromQuery] int? olderThan = null,
                                             [FromQuery] int? youngerThan = null)
        {
            AbstractSpecification<User> spec = AbstractSpecification<User>.Default;

            if (username != null)
            {
                spec = spec.And(UsernameShould.Be(username));
            }

            if (gender != null)
            {
                spec = spec.And(GenderShould.Be(gender.Value));
            }

            if (olderThan != null)
            {
                spec = spec.And(AgeShould.BeGreaterThan(olderThan.Value));
            }

            if (youngerThan != null)
            {
                spec = spec.And(AgeShould.BeLessThan(youngerThan.Value));
            }

            IEnumerable<User> users = await _userRepository.GetUsersAsync(spec);

            if (users.Any())
            {
                return StatusCode((int) HttpStatusCode.OK, users);
            }

            return StatusCode((int) HttpStatusCode.NotFound);
        }
    }
}