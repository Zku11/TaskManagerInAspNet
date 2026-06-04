using Microsoft.EntityFrameworkCore;
using TaskManagerInAspNet.Entities;

namespace TaskManagerInAspNet
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<UserTask> UserTasks { get; set; }
    }
}
