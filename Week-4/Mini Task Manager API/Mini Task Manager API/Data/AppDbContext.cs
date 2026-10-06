using Microsoft.EntityFrameworkCore;
using Mini_Task_Manager_API.Models;

namespace Mini_Task_Manager_API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<TaskItem> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
         
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.id);

                entity.Property(u => u.username).IsRequired().HasMaxLength(50);

                entity.Property(u => u.passwordHash).IsRequired();

                entity.Property(u => u.role).IsRequired().HasMaxLength(20);

                entity.HasIndex(u => u.username).IsUnique();

            });

            
            modelBuilder.Entity<TaskItem>(entity =>
            {
                entity.HasKey(t => t.id);

                entity.Property(t => t.title).IsRequired().HasMaxLength(100);

                entity.Property(t => t.description).HasMaxLength(500);

                entity.Property(t => t.ownerUsername).IsRequired().HasMaxLength(50);
               
            });
        }
    }
}
