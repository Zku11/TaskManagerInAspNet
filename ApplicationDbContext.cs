using Microsoft.EntityFrameworkCore;
using TaskManagerInAspNet.Entities;

namespace TaskManagerInAspNet
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<UserTask> UserTasks { get; set; }
        public DbSet<Step> Steps { get; set; }
    }
}
