using Microsoft.EntityFrameworkCore;
using UserManagement.Api.Models;

namespace UserManagement.Api.Db
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; } = null!;
    }
}