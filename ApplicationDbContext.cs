using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManagerInAspNet.Entities;

namespace TaskManagerInAspNet
{
    public class ApplicationDbContext : IdentityDbContext
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
        public DbSet<AttachedFile> AttachedFiles { get; set; }
    }
}
