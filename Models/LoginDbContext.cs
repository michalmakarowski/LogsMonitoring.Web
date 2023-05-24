using Microsoft.EntityFrameworkCore;

namespace LogsMonitoring.Web.Models
{
    public class LoginDbContext : DbContext
    {
        public LoginDbContext(DbContextOptions<LoginDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
